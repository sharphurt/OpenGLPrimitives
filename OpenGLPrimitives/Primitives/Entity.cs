using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives
{
    public abstract class Entity
    {
        protected Face[] Faces;

        protected PrimitiveType PrimitiveType = PrimitiveType.Polygon;

        private Polygon[] _polygons;

        public Polygon[] Polygons
        {
            get => _polygons ?? (_polygons = Faces.Select(f => new Polygon(f, PrimitiveType)).ToArray());
            protected set => _polygons = value;
        }
    }
}