using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Octahedron : Entity
    {
        public Octahedron()
        {
            DrawMethod = PrimitiveType.TriangleStrip;
            var basic = new[]
            {
                new Vector4(0, 0, 1, 1),
                new Vector4(0, 0, -1, 1),
                new Vector4(-1, -1, 0, 1),
                new Vector4(1, -1, 0, 1),
                new Vector4(1, 1, 0, 1),
                new Vector4(-1, 1, 0, 1)
            };

            var faces = new[]
            {
                new Face(basic[0], basic[3], basic[4]),
                new Face(basic[0], basic[4], basic[5]),
                new Face(basic[0], basic[5], basic[2]),
                new Face(basic[0], basic[2], basic[3]),
                new Face(basic[1], basic[4], basic[3]),
                new Face(basic[1], basic[5], basic[4]),
                new Face(basic[1], basic[2], basic[5]),
                new Face(basic[1], basic[3], basic[2])
            };
            
            Vertices = VectorUtils.FacesToVertices(faces, 1);
        }
    }
}