using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Primitives
{
    public class SimplePolygon : Entity
    {
        public SimplePolygon(Vector4[] vertices)
        {
            Faces = new[] {new Face(vertices)};
        }
    }
}