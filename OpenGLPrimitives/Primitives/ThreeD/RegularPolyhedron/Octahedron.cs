using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Octahedron : Entity
    {
        private readonly Vector4[] _points =
        {
            new Vector4(0, 0, 0.5f, 1),
            new Vector4(0, 0, -0.5f, 1),
            new Vector4(-0.5f, -0.5f, 0, 1),
            new Vector4(0.5f, -0.5f, 0, 1),
            new Vector4(0.5f, 0.5f, 0, 1),
            new Vector4(-0.5f, 0.5f, 0, 1)
        };

        private readonly int[][] _indeces =
        {
            new[] {0, 3, 4},
            new[] {0, 4, 5},
            new[] {0, 5, 2},
            new[] {0, 2, 3},
            new[] {1, 4, 3},
            new[] {1, 5, 4},
            new[] {1, 2, 5},
            new[] {1, 3, 2}
        };

        public Octahedron()
        {
            Faces = _indeces.Select(f => new Face(f.Select(i => _points[i]).ToArray())).ToArray();
        }
    }
}