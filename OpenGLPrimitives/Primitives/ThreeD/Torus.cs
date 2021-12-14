using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Torus : Entity
    {
        public Torus()
        {
            Vertices = Geometry.Geometry.CreateTorus(0.5f);
            DrawMethod = PrimitiveType.QuadStrip;
        }
    }
}