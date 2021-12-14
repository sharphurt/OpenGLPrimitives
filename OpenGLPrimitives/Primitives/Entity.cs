using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives
{
    public abstract class Entity
    {
        public Vertex[] Vertices;

        public PrimitiveType DrawMethod { get; protected set; }
    }
}