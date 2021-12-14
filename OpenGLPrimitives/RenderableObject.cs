using System;
using OpenGLPrimitives.Primitives;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives
{
    public abstract class RenderableObject : IDisposable
    {
        protected readonly Entity Entity; 
        protected readonly int VertexArray;
        protected readonly int Buffer;

        protected RenderableObject(Entity entity)
        {
            Entity = entity;
            VertexArray = GL.GenVertexArray();
            Buffer = GL.GenBuffer();

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffer);
        }
        
        public void Bind()
        {
            GL.BindVertexArray(VertexArray);
        }
        
        public void Render()
        {
           // GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            GL.DrawArrays(Entity.DrawMethod, 0, Entity.Vertices.Length);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                GL.DeleteVertexArray(VertexArray);
                GL.DeleteBuffer(Buffer);
            }
        }
    }
}