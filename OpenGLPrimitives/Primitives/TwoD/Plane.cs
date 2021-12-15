using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.TwoD
{
    public class Plane : Entity
    {
        private readonly Vector4[] _points =
        {
            new Vector4(-0.5f, -0.5f, 0, 1),
            new Vector4(-0.5f, 0.5f, 0, 1),
            new Vector4(0.5f, 0.5f, 0, 1),
            new Vector4(0.5f, -0.5f, 0, 1),
        };

        private readonly int[] _indices = {0, 1, 2, 3};

        public Plane()
        {
            Faces = new[] {new Face(_indices.Select(i => _points[i]).ToArray()),};
        }
    }
}