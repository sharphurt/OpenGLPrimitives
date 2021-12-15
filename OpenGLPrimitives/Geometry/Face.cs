using System;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Geometry
{
    public struct Face
    {
        public Vector4[] Vertices { get; }

        public Vector4 A
        {
            get => Vertices[0];
        }

        public Vector4 B
        {
            get => Vertices[1];
        }

        public Vector4 C
        {
            get => Vertices[2];
        }

        public Vector4 Normal => new Vector4(Vector3.Cross((B - C).Xyz, (B - A).Xyz).Normalized(), 1);

        public Face(params Vector4[] vertices)
        {
            Vertices = vertices;
        }
    }
}