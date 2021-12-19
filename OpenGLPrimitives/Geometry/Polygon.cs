using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Buffer = System.Buffer;

namespace OpenGLPrimitives.Geometry
{
    public class Polygon
    {
        private readonly Vertex[] Vertices;
        
        private int VertexArray { get; set; }
        private int Buffer { get; set; }

        public Polygon(Vertex[] vertices)
        {
            Vertices = vertices;
            InitializeBuffers();
        }

        private void InitializeBuffers()
        {
            VertexArray = GL.GenVertexArray();
            Buffer = GL.GenBuffer();

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArray);

            GL.NamedBufferStorage(Buffer, Vertex.Size * Vertices.Length, Vertices, BufferStorageFlags.MapWriteBit);

            GL.VertexArrayAttribBinding(VertexArray, 0, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 0);
            GL.VertexArrayAttribFormat(VertexArray, 0, 2, VertexAttribType.Float, false, 0);

            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, Vertex.Size);

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffer);
        }

        public void Render()
        {
            GL.BindVertexArray(VertexArray);
            GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length);
        }
    }
}