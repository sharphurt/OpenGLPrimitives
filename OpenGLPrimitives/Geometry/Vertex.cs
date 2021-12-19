﻿using System.Drawing;
using OpenTK;

namespace OpenGLPrimitives
{
    public struct Vertex
    {
        public const int Size = (2 + 2) * 4;

        public Vector2 Position { get; }
        
        public Vector2 TexCoord { get; }

        public Vertex(Vector2 position, Vector2 texCoord)
        {
            Position = position;
            TexCoord = texCoord;
        }
    }
}