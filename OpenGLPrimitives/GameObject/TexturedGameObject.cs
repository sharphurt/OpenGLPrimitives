using System;
using System.Collections.Generic;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives
{
    public class TexturedGameObject : Object
    {
        public Mesh[] Meshes { get; }

        public TexturedGameObject(Mesh[] meshes, Vector4 position, Vector3 rotation, Vector3 scale) : base(position,
            rotation, scale)
        {
            Meshes = meshes;
        }
    }
}