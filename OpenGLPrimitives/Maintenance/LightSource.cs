using System;
using OpenGLPrimitives.Primitives.ThreeD;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives
{
    public class LightSource
    {
        public Vector4 Position { get; set; }

        public Vector4 Color { get; }
        
        public LightSource(Vector4 position, Vector4 color)
        {
            Position = position;
            Color = color;
        }

        public void Apply(Shader shader, bool show)
        {
            shader.SetVec4("lightPos", Position);
            shader.SetVec4("lightColor", Color);
            shader.SetFloat("lightEnable", 1);

            shader.Disable();
            
            if (show)
            {
                GL.PointSize(10);
                GL.Begin(PrimitiveType.Points);
                GL.Color3(System.Drawing.Color.Azure);
                GL.Vertex3(Position.Xyz);
                GL.End();
            }

            shader.Use();
        }

        public void Disable(Shader shader)
        {
            shader.SetFloat("lightEnable", -1);
        }
    }
}