using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Buffer = System.Buffer;

namespace OpenGLPrimitives.Geometry
{
    public class Polygon : IDisposable
    {
        public Vertex[] Vertices;

        private PrimitiveType _primitiveType;

        private int VertexArray { get; set; }
        private int Buffer { get; set; }

        public Vector4 Normal { get; }

        public Polygon(Face face, Vector4 normal, PrimitiveType primitiveType)
        {
            _primitiveType = primitiveType;
            Vertices = face.Vertices.Select(v => new Vertex(v, normal.Normalized())).ToArray();
            Normal = normal.Normalized();
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
            GL.VertexArrayAttribFormat(VertexArray, 0, 4, VertexAttribType.Float, false, 0);

            GL.VertexArrayAttribBinding(VertexArray, 1, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 1);
            GL.VertexArrayAttribFormat(VertexArray, 1, 4, VertexAttribType.Float, false, 16);

            GL.VertexArrayAttribBinding(VertexArray, 2, 0);
            GL.EnableVertexArrayAttrib(VertexArray, 2);
            GL.VertexArrayAttribFormat(VertexArray, 2, 4, VertexAttribType.Float, false, 32);

            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, Vertex.Size);

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffer);
        }

        public void Bind()
        {
            GL.BindVertexArray(VertexArray);
        }

        public void Render()
        {
            GL.DrawArrays(_primitiveType, 0, Vertices.Length);
        }

        public void Dispose()
        {
        }
    }
}