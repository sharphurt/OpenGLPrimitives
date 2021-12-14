using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Icosahedron : Entity
    {
        private readonly Vector4[] _points =
        {
            new Vector4(0, -0.525f, 0.85f, 1),
            new Vector4(0.85f, 0, 0.525f, 1),
            new Vector4(0.85f, 0, -0.525f, 1),
            new Vector4(-0.85f, 0, -0.525f, 1),
            new Vector4(-0.85f, 0, 0.525f, 1),
            new Vector4(-0.525f, 0.85f, 0, 1),
            new Vector4(0.525f, 0.85f, 0, 1),
            new Vector4(0.525f, -0.85f, 0, 1),
            new Vector4(-0.525f, -0.85f, 0, 1),
            new Vector4(0, -0.525f, -0.85f, 1),
            new Vector4(0, 0.525f, -0.85f, 1),
            new Vector4(0, 0.525f, 0.85f, 1)
        };

        private readonly int[][] _indices =
        {
            new[] {1, 2, 6},
            new[] {1, 7, 2},
            new[] {3, 4, 5},
            new[] {4, 3, 8},
            new[] {6, 5, 11},
            new[] {5, 6, 10},
            new[] {9, 10, 2},
            new[] {10, 9, 3},
            new[] {7, 8, 9},
            new[] {8, 7, 0},
            new[] {11, 0, 1},
            new[] {0, 11, 4},
            new[] {6, 2, 10},
            new[] {1, 6, 11},
            new[] {3, 5, 10},
            new[] {5, 4, 11},
            new[] {2, 7, 9},
            new[] {7, 1, 0},
            new[] {3, 9, 8},
            new[] {4, 8, 0}
        };
        
        public Icosahedron()
        {
            Faces = _indices.Select(f => new Face(f.Select(i => _points[i]).ToArray())).ToArray();
        }
    }
}