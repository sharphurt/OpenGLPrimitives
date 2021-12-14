using System;
using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Dodecahedron : Entity
    {
        public Dodecahedron()
        {
            var vertices = new[]
            {
                new Vector4(1, 1, 1, 1),
                new Vector4(1, 1, -1, 1),
                new Vector4(1, -1, 1, 1),
                new Vector4(1, -1, -1, 1),
                new Vector4(-1, 1, 1, 1),
                new Vector4(-1, 1, -1, 1),
                new Vector4(-1, -1, 1, 1),
                new Vector4(-1, -1, -1, 1),
                new Vector4(0, 0.618f, 1.618f, 1),
                new Vector4(0, 0.618f, -1.618f, 1),
                new Vector4(0, -0.618f, 1.618f, 1),
                new Vector4(0, -0.618f, -1.618f, 1),
                new Vector4(0.618f, 1.618f, 0, 1),
                new Vector4(0.618f, -1.618f, 0, 1),
                new Vector4(-0.618f, 1.618f, 0, 1),
                new Vector4(-0.618f, -1.618f, 0, 1),
                new Vector4(1.618f, 0, 0.618f, 1),
                new Vector4(1.618f, 0, -0.618f, 1),
                new Vector4(-1.618f, 0, 0.618f, 1),
                new Vector4(-1.618f, 0, -0.618f, 1)
            };

            var faces = new[]
            {
                new[] {0, 16, 2, 10, 8},
                new[] {0, 8, 4, 14, 12},
                new[] {16, 17, 1, 12, 0},
                new[] {1, 9, 11, 3, 17},
                new[] {1, 12, 14, 5, 9},
                new[] {2, 13, 15, 6, 10},
                new[] {13, 3, 17, 16, 2},
                new[] {3, 11, 7, 15, 13},
                new[] {4, 8, 10, 6, 18},
                new[] {14, 5, 19, 18, 4},
                new[] {5, 19, 7, 11, 9},
                new[] {15, 7, 19, 18, 6}
            };

            var v = new List<Vertex>();

            for (int i = 0; i < 12; i++)
            {
                for (int x = 0; x < 5; x++)
                    v.Add(new Vertex(vertices[faces[i][x]], new Vector4(vertices[faces[i][x]])));
            }

            Vertices = v.ToArray();
            
            /*
            Vertices = VectorUtils.FacesToVertices(faces.Select(f => new Face(f.Select(i => vertices[i]).ToArray()))
                .ToArray(), 1);*/
            DrawMethod = PrimitiveType.TriangleFan;
        }

        private static List<Vertex> MakeDodecahedron(float r)
        {
            // Calculate constants that will be used to generate vertices
            var phi = (float) (Math.Sqrt(5) - 1) / 2; // The golden ratio

            var a = (float) (1 / Math.Sqrt(3));
            var b = a / phi;
            var c = a * phi;

            // Generate each vertex
            var vertices = new List<Vertex>();
            foreach (var i in new[] {-1, 1})
            {
                foreach (var j in new[] {-1, 1})
                {
                    vertices.Add(new Vertex(new Vector4(0, i * c * r, j * b * r, 1), new Vector4(i * c, j * b, 1, 1)));
                    vertices.Add(new Vertex(new Vector4(i * c * r, j * b * r, 0, 1), new Vector4(i * c, j * b, 0, 1)));
                    vertices.Add(new Vertex(new Vector4(i * b * r, 0, j * c * r, 1), new Vector4(i * b, 0, j * c, 1)));

                    vertices.AddRange(new[] {-1, 1}.Select(k =>
                        new Vertex(new Vector4(i * a * r, j * a * r, k * a * r, 1),
                            new Vector4(i * a, j * a, k * a, 1))));
                }
            }

            return vertices;
        }
    }
}