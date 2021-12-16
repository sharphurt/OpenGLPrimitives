using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenGLPrimitives.Geometry;

namespace OpenGLPrimitives.Parser
{
    public static class ObjLoader
    {
        public static List<Mesh> LoadModel(string folderPath)
        {
            var data = LoadData(folderPath);
            if (data.mtl == null || data.texturePath == null)
                return ObjParser.Parse(data.obj);

            return ObjParser.Parse(data.obj, data.mtl, data.texturePath);
        }

        private static (List<string> obj, List<string> mtl, string texturePath) LoadData(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            var directories = Directory.GetDirectories(folderPath);

            if (!files.Any(f => f.EndsWith(".obj")))
                throw new FileLoadException($".obj file not found in path: {folderPath}");

            var objPath = files.First(f => f.EndsWith(".obj"));
            var obj = ReadFile(objPath);
            
            var useMtl = obj.data.Any(l => l.Contains("mtllib")) && files.Any(f => f.EndsWith(".mtl"));

            if (!useMtl)
                return (obj.data, null, null);

            var mtlPath = files.First(f => f.EndsWith(".mtl"));
            var mtl = ReadFile(mtlPath);

            var useTextures = mtl.data.Any(l => l.Contains("map_Kd"));

            if (useTextures && !directories.Any(d => d.Contains("Textures") || d.Contains("textures")))
                throw new DirectoryNotFoundException($"Textures directory not found in path: {folderPath}");

            return !useTextures
                ? (obj.data, mtl.data, null)
                : (obj.data, mtl.data, directories.First(d => d.Contains("Textures") || d.Contains("textures")));
        }

        private static (string name, List<string> data) ReadFile(string path)
        {
            var name = Path.GetFileName(path);
            var data = File.ReadAllLines(path).ToList();
            return (name, data);
        }
    }
}