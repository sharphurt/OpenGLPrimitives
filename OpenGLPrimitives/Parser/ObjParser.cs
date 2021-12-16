using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Parser
{
    public static class ObjParser
    {
        public static List<Mesh> Parse(List<string> obj, List<string> mtl, string texturesFolder)
        {
            var points = ParseVectors3(obj, "v ");
            var normals = ParseVectors3(obj, "vn ");
            var textureCoodinates = ParseVectors2(obj, "vt ");

            var mtls = MtlParser.ParseMtl(mtl);
            var meshes = new List<Mesh>();

            var lineIndex = 0;
            while (lineIndex < obj.Count)
            {
                if (obj[lineIndex].StartsWith("usemtl"))
                {
                    var currentMtl = mtls.Find(mtlData => mtlData.Name == obj[lineIndex].Split(' ')[1]);
                    var faceLines = GetMtlFaces(obj, lineIndex);
                    var faces = ParseFaces(faceLines);
                    var (vertices, polygons) = CombinePolygons(points, normals, textureCoodinates, faces);
                    meshes.Add(new Mesh(vertices.ToArray(), polygons.ToArray(),
                        $"{texturesFolder}/{currentMtl.TexturePath}"));
                    lineIndex += faceLines.Count;
                }
                else
                    lineIndex++;
            }

            return meshes;
        }

        public static List<Mesh> Parse(List<string> objData)
        {
            var points = ParseVectors3(objData, "v ");
            var normals = ParseVectors3(objData, "vn ");
            var faces = ParseFaces(objData);

            var (vertices, polygons) = CombinePolygons(points, normals, faces);

            return new List<Mesh> {new Mesh(vertices.ToArray(), polygons.ToArray())};
        }

        private static List<string> GetMtlFaces(List<string> data, int startIndex)
        {
            var nextMeshStart = data.FindIndex(startIndex + 1, l => l.StartsWith("usemtl"));
            return nextMeshStart == -1
                ? data.GetRange(startIndex, data.Count - startIndex)
                : data.GetRange(startIndex, nextMeshStart - startIndex);
        }


        private static List<List<(int, int, int)>> ParseFaces(IEnumerable<string> data) =>
            data.Where(l => l.StartsWith("f "))
                .Select(l => l.Replace("f ", "")
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(ParseVertex)
                    .ToList())
                .ToList();

        private static List<Vector4> ParseVectors3(IEnumerable<string> data, string start) =>
            data.Where(l => l.StartsWith(start))
                .Select(l => l.Replace(start, "")
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => float.Parse(c, CultureInfo.InvariantCulture))
                    .ToList())
                .Select(l => new Vector4(l[0], l[1], l[2], 1))
                .ToList();

        private static List<Vector2> ParseVectors2(IEnumerable<string> data, string start) =>
            data.Where(l => l.StartsWith(start))
                .Select(l => l.Replace(start, "")
                    .Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => float.Parse(c, CultureInfo.InvariantCulture))
                    .ToList())
                .Select(l => new Vector2(l[0], l[1]))
                .ToList();

        private static (int pointIndex, int textureIndex, int normalIndex) ParseVertex(string str)
        {
            var vertex = str.Split('/');
            if (vertex.Length == 1)
                return (int.Parse(vertex[0]), 0, 0);

            var textureIndex = 0;
            var normalIndex = 0;

            if (vertex.Length > 1 && !string.IsNullOrEmpty(vertex[1]))
                textureIndex = int.Parse(vertex[1]);
            if (vertex.Length > 2 && !string.IsNullOrEmpty(vertex[2]))
                normalIndex = int.Parse(vertex[2]);

            return (int.Parse(vertex[0]), normalIndex, textureIndex);
        }

        private static (List<Vertex>, List<Polygon>) CombinePolygons(List<Vector4> points, List<Vector4> normals,
            List<List<(int point, int normal, int texture)>> faceIndexes)
        {
            var result = (new List<Vertex>(), new List<Polygon>());
            foreach (var v in faceIndexes.Select(faceIndex => faceIndex.Select(i =>
            {
                Vertex vertex = new Vertex(points[i.point - 1]);
                vertex.Normal = i.normal != 0 ? normals[i.normal - 1] : vertex.Position.Normalized();

                return vertex;
            }).ToList()))
            {
                result.Item1.AddRange(v);
                result.Item2.Add(new Polygon(v.ToArray()));
            }

            return result;
        }

        private static (List<Vertex>, List<Polygon>) CombinePolygons(List<Vector4> points, List<Vector4> normals,
            List<Vector2> textures,
            List<List<(int point, int normal, int texture)>> faceIndexes)
        {
            var result = (new List<Vertex>(), new List<Polygon>());
            foreach (var v in faceIndexes.Select(faceIndex => faceIndex.Select(i =>
            {
                var point = i.point < 0 ? points.Count + i.point : i.point - 1;

                Vertex vertex = new Vertex(points[point]);

                if (i.texture != 0)
                {
                    var texture = i.texture < 0 ? textures.Count + i.texture : i.texture - 1;
                    vertex.TextureCoordinate = textures[texture];
                }

                if (i.normal != 0)
                {
                    var normal = i.normal < 0 ? normals.Count + i.normal : i.normal - 1;
                    vertex.Normal = normals[normal];
                }
                else
                    vertex.Normal = vertex.Position.Normalized();

                return vertex;
            }).ToList()))
            {
                result.Item1.AddRange(v);
                result.Item2.Add(new Polygon(v.ToArray()));
            }

            return result;
        }
    }

    public static class MtlParser
    {
        public static List<MtlData> ParseMtl(List<string> data)
        {
            var mtls = new List<MtlData>();

            string currentName = "";
            foreach (var l in data.Where(l => l.Contains("newmtl") || l.Contains("map_Kd")))
            {
                if (l.Contains("newmtl"))
                {
                    currentName = l.Split(' ')[1];
                    continue;
                }

                if (l.Contains("map_Kd"))
                {
                    var currentTexturePath = l.Split(' ')[1];
                    mtls.Add(new MtlData(currentName, currentTexturePath));
                }
            }

            return mtls;
        }
    }
}