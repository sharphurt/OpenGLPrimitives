using System;
using System.Drawing;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Maintenance;
using OpenTK.Graphics.OpenGL;
using Buffer = System.Buffer;
using TextureUnit = OpenTK.Graphics.OpenGL4.TextureUnit;

namespace OpenGLPrimitives.Geometry
{
    public class Mesh : IDisposable
    {
        public Polygon[] Polygons { get; }
        
        public Vertex[] Vertices { get; }
        
        public Texture Texture { get; }

        private PrimitiveType _primitiveType;

        private int VertexArray { get; set; }
        private int Buffer { get; set; }

        public Mesh(Vertex[] vertices, Polygon[] polygons, string texturePath)
        {
            Vertices = vertices;
            Polygons = polygons;
            Texture = Texture.LoadFromFile(texturePath);
            InitializeBuffers();
            _primitiveType = GetPrimitiveType(Polygons[0]);

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
            GL.VertexArrayAttribFormat(VertexArray, 2, 2, VertexAttribType.Float, false, 32);

            GL.VertexArrayVertexBuffer(VertexArray, 0, Buffer, IntPtr.Zero, Vertex.Size);

            GL.BindVertexArray(VertexArray);
            GL.BindBuffer(BufferTarget.ArrayBuffer, Buffer);
        }

        

        public void Bind()
        {
            GL.BindVertexArray(VertexArray);
            Texture.Use(TextureUnit.Texture0);
        }
        
        private PrimitiveType GetPrimitiveType(Polygon polygon)
        {
            switch (polygon.Vertices.Length)
            {
                case 1: return PrimitiveType.Points;
                case 2: return PrimitiveType.Points;
                case 3: return PrimitiveType.Triangles;
                case 4: return PrimitiveType.Quads;
                default: return PrimitiveType.Polygon;
            }
        }
        
        public void Render(ICamera camera, LightSource light, Shader shader)
        {
            Bind();
            GL.DrawArrays(_primitiveType, 0, Vertices.Length);
        }
        
        public void Dispose()
        {
        }
    }
}