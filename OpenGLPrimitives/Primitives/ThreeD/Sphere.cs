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
        public Sphere(int uRes, int vRes)
        {
            Faces = Geometry.Geometry.CreateSphere(uRes, vRes).ToArray();
            PrimitiveType = PrimitiveType.TriangleStrip;
        }
    }
}