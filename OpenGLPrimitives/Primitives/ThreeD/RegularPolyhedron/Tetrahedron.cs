using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Tetrahedron : Entity
    {
        private readonly Vector4[] _points =
        {
            new Vector4(0.5f, 0.5f, 0.5f, 1),
            new Vector4(-0.5f, -0.5f, 0.5f, 1),
            new Vector4(0.5f, -0.5f, -0.5f, 1),
            new Vector4(-0.5f, 0.5f, -0.5f, 1)
        };

        private readonly int[][] _indices =
        {
            new[] {0, 1, 2},
            new[] {1, 0, 3},
            new[] {1, 3, 2},
            new[] {0, 2, 3}
        };

        public Tetrahedron()
        {
            Faces = _indices.Select(f => new Face(f.Select(i => _points[i] * 0.5f).ToArray())).ToArray();
        }
    }
}