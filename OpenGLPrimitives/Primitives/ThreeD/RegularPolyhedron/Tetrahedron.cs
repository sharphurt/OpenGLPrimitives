using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Tetrahedron : Entity
    {
        public Tetrahedron()
        {
            var basics = new[]
            {
                new Vector4(1, 1, 1, 1),
                new Vector4(-1, -1, 1, 1),
                new Vector4(1, -1, -1, 1),
                new Vector4(-1, 1, -1, 1)
            };

            var faces = new[]
            {
                new Face(basics[0], basics[1], basics[2]),
                new Face(basics[1], basics[0], basics[3]),
                new Face(basics[1], basics[3], basics[2]),
                new Face(basics[0], basics[2], basics[3])
            };

            DrawMethod = PrimitiveType.Triangles;
            Vertices = VectorUtils.FacesToVertices(faces, 1);
        }
    }
}