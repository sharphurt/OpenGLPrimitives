﻿using System;
 using System.Drawing;
using OpenTK;

namespace OpenGLPrimitives
{
    public struct Vertex
    {
        public const int Size = (4 + 4 + 2) * 4;

        public Vector4 Position { get; }
        public Vector4 Normal { get; set; }
        public Vector2 Texture { get; set; }

        public Vertex(Vector4 position)
        {
            Position = position;
            Normal = Vector4.Zero;
            Texture = Vector2.Zero;
        }

        public Vertex(Vector4 position, Vector4 normal) : this(position)
        {
            Normal = normal;
            Texture = Vector2.Zero;
        }
    }
}