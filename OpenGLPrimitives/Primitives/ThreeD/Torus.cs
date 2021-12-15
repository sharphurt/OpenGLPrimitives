using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Torus : Entity
    {
        public Torus()
        {
            PrimitiveType = PrimitiveType.QuadStrip;
            Faces = Geometry.Geometry.CreateTorus(50, 50);
        }
    }
}