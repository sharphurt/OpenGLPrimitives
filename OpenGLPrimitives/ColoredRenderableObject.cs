using System;
using OpenGLPrimitives.Primitives;
using OpenTK.Graphics.OpenGL4;

namespace OpenGLPrimitives
{
    public class ColoredRenderableObject : RenderableObject
    {
        public ColoredRenderableObject(Entity entity) : base(entity)
        {
            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexArray);

            GL.NamedBufferStorage(Buffer, Vertex.Size * Entity.Vertices.Length, Entity.Vertices, BufferStorageFlags.MapWriteBit);

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
        }
    }
}