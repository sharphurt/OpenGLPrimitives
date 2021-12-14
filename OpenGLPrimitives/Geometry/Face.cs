using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Geometry
{
    public class Face
    {
        public Vector4[] Vertices { get; }

        public Vector4 A
        {
            get => Vertices[0];
            set => Vertices[0] = value;
        }

        public Vector4 B
        {
            get => Vertices[1];
            set => Vertices[1] = value;
        }

        public Vector4 C
        {
            get => Vertices[2];
            set => Vertices[2] = value;
        }

        public Vector3 Normal => Vector3.Cross((B - C).Xyz, (B - A).Xyz);

        public Face(params Vector4[] vertices)
        {
            Vertices = vertices;
        }

        public void Draw()
        {
            GL.Begin(PrimitiveType.Polygon);
            foreach (var vertex in Vertices)
            {
                GL.Normal3(Normal);
                GL.Vertex4(vertex);
            }
        }
    }
}