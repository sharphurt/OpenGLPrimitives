using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Pyramid : Entity
    {
        private readonly Vector4[] _points =
        {
            new Vector4(0, 1, 0, 1),
            new Vector4(-1, -1, 1, 1),
            new Vector4(1, -1, 1, 1),
            new Vector4(1, -1, -1, 1),
            new Vector4(-1, -1, -1, 1)
        };

        private readonly int[][] _indices =
        {
            new[] {0, 2, 3},
            new[] {0, 1, 2},
            new[] {0, 4, 1},
            new[] {0, 3, 4},
            new[] {1, 2, 3},
            new[] {3, 4, 1}
        };

        public Pyramid()
        {
            Faces = _indices.Select(f => new Face(f.Select(i => _points[i]).ToArray())).ToArray();
        }
    }
}