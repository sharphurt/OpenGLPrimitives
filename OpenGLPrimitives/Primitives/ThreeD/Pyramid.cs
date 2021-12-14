using System.Collections.Generic;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Pyramid : Entity
    {
        private int[][] _faces =
        {
            new[] {3, 2, 0},
            new[] {2, 1, 0},
            new[] {1, 4, 0},
            new[] {4, 3, 0},
            new[] {1, 2, 3},
            new[] {3, 4, 1}
        };

        private List<Vector3> _points = new List<Vector3>
        {
            /*0*/ new Vector3(0, 1, 0),
            /*1*/ new Vector3(-1, -1, 1),
            /*2*/ new Vector3(1, -1, 1),
            /*3*/ new Vector3(1, -1, -1),
            /*4*/ new Vector3(-1, -1, -1)
        };

        public Pyramid()
        {
            var vertices = new List<Vertex>();
            foreach (var face in _faces)
            {
                var v1 = _points[face[0]];
                var v2 = _points[face[1]];
                var v3 = _points[face[2]];

                var normal = Vector3.Cross(v2 - v1, v2 - v3);

                vertices.AddRange(face.Select(index => _points[index]).Select(v =>
                    new Vertex(new Vector4(v, 1), new Vector4(normal, 1))));

                Vertices = vertices.ToArray();
            }

            DrawMethod = PrimitiveType.Triangles;
        }
    }
}