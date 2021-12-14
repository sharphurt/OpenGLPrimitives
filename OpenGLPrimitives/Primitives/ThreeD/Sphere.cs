using System;
using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Sphere : Entity
    {
        public Sphere()
        {
            DrawMethod = PrimitiveType.QuadStrip;
            Vertices = Geometry.Geometry.CreateSphere(50, 50).ToArray();
        }
    }
}