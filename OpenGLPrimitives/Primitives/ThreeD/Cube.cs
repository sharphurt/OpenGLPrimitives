using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Cube : Entity
    {
        public Cube()
        {
            DrawMethod = PrimitiveType.Quads;
            Vertices = new[]
            {
                new Vertex(new Vector4(-1, 1, -1, 1), Vector4.UnitY),
                new Vertex(new Vector4(1, 1, -1, 1), Vector4.UnitY),
                new Vertex(new Vector4(1, 1, 1, 1), Vector4.UnitY),
                new Vertex(new Vector4(-1, 1, 1, 1), Vector4.UnitY),
                
                new Vertex(new Vector4(-1, -1, -1, 1), -Vector4.UnitY),
                new Vertex(new Vector4(1, -1, -1, 1), -Vector4.UnitY),
                new Vertex(new Vector4(1, -1, 1, 1), -Vector4.UnitY),
                new Vertex(new Vector4(-1, -1, 1, 1), -Vector4.UnitY),

                new Vertex(new Vector4(-1, 1, 1, 1), Vector4.UnitZ),
                new Vertex(new Vector4(1, 1, 1, 1), Vector4.UnitZ),
                new Vertex(new Vector4(1, -1, 1, 1), Vector4.UnitZ),
                new Vertex(new Vector4(-1, -1, 1, 1), Vector4.UnitZ),
                
                new Vertex(new Vector4(-1, -1, -1, 1), -Vector4.UnitZ),
                new Vertex(new Vector4(1, -1, -1, 1), -Vector4.UnitZ),
                new Vertex(new Vector4(1, 1, -1, 1), -Vector4.UnitZ),
                new Vertex(new Vector4(-1, 1, -1, 1), -Vector4.UnitZ),
                
                new Vertex(new Vector4(-1, 1, 1, 1), -Vector4.UnitX),
                new Vertex(new Vector4(-1, 1, -1, 1), -Vector4.UnitX),
                new Vertex(new Vector4(-1, -1, -1, 1), -Vector4.UnitX),
                new Vertex(new Vector4(-1, -1, 1, 1), -Vector4.UnitX),

                new Vertex(new Vector4(1, 1, 1, 1), Vector4.UnitX),
                new Vertex(new Vector4(1, 1, -1, 1), Vector4.UnitX),
                new Vertex(new Vector4(1, -1, -1, 1), Vector4.UnitX),
                new Vertex(new Vector4(1, -1, 1, 1), Vector4.UnitX)
            };
        }
    }
}