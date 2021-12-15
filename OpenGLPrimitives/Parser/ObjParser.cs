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
        public static List<Mesh> Parse(string objPath, string mtlPath, string texturesFolder)
        {
            var (name, data) = ReadFile(objPath);
            var points = ParseVectors3(data, "v ");
            var normals = ParseVectors3(data, "vn ");
            var textureCoodinates = ParseVectors2(data, "vt ");

            var mtls = MtlParser.ParseMtl(mtlPath);
            var meshes = new List<Mesh>();


            var lineIndex = 0;
            while (lineIndex < data.Count)
            {
                if (data[lineIndex].StartsWith("usemtl"))
                {
                    var currentMtl = mtls.Find(mtlData => mtlData.Name == data[lineIndex].Split(' ')[1]);
                    var faceLines = GetMtlFaces(data, lineIndex);
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

        public static List<Mesh> Parse(string objPath)
        {
            var (name, data) = ReadFile(objPath);
            var points = ParseVectors3(data, "v ");
            var normals = ParseVectors3(data, "vn ");
            var faces = ParseFaces(data);

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

        public static (string name, List<string> data) ReadFile(string path)
        {
            var name = Path.GetFileName(path);
            var data = File.ReadAllLines(path).ToList();
            return (name, data);
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
                return (int.Parse(vertex[0]), -1, -1);

            var textureIndex = -1;
            var normalIndex = -1;

            if (!string.IsNullOrEmpty(vertex[1]))
                textureIndex = int.Parse(vertex[1]);
            if (!string.IsNullOrEmpty(vertex[2]))
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
                vertex.Normal = i.normal != -1 ? normals[i.normal - 1] : vertex.Position.Normalized();
                
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
                Vertex vertex = new Vertex(points[i.point - 1]);
                if (i.texture != -1)
                    vertex.TextureCoordinate = textures[i.texture - 1];
                
                vertex.Normal = i.normal != -1 ? normals[i.normal - 1] : vertex.Position.Normalized();
                
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
        public static List<MtlData> ParseMtl(string path)
        {
            var (filename, data) = ObjParser.ReadFile(path);

            var mtls = new List<MtlData>();

            string currentName = "";
            foreach (var l in data.Where(l => l.StartsWith("newmtl") || l.StartsWith("map_Kd")))
            {
                if (l.StartsWith("newmtl"))
                {
                    currentName = l.Split(' ')[1];
                    continue;
                }

                if (l.StartsWith("map_Kd"))
                {
                    var currentTexturePath = l.Split(' ')[1];
                    mtls.Add(new MtlData(currentName, currentTexturePath));
                }
            }

            return mtls;
        }
    }
}