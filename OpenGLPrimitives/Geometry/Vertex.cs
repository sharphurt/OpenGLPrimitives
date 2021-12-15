﻿using System.Drawing;
using OpenTK;

namespace OpenGLPrimitives
{
    public struct Vertex
    {
        public const int Size = (4 + 4 + 4) * 4;

        public Vector4 Position { get; }
        public Vector4 Normal { get; }
        public Vector4 Color { get; }

        public Vertex(Vector4 position, Vector4 normal)
        {
            Normal = normal;
            Position = position;
            Color = ColorUtils.GenerateRandomColor();
        }
    }
}