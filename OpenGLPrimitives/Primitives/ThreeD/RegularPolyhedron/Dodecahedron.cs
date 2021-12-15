using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Dodecahedron : Entity
    {
        private readonly Vector4[] _points =
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

        private readonly int[][] _indices =
        {
            new[] {8, 10, 2, 16, 0},
            new[] {12, 14, 4, 8, 0},
            new[] {16, 17, 1, 12, 0},
            new[] {17, 3, 11, 9, 1},
            new[] {9, 5, 14, 12, 1},
            new[] {10, 6, 15, 13, 2},
            new[] {2, 16, 17, 3, 13},
            new[] {13, 15, 7, 11, 3},
            new[] {18, 6, 10, 8, 4},
            new[] {14, 5, 19, 18, 4},
            new[] {9, 11, 7, 19, 5},
            new[] {6, 18, 19, 7, 15},
        };


        public Dodecahedron()
        {
            Faces = _indices.Select(f => new Face(f.Select(i => _points[i]).ToArray())).ToArray();
        }
    }
}