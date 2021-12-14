using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Utils
{
    public static class VectorUtils
    {
        public static Vector4 MiddlePoint(Vector4 vec1, Vector4 vec2) => (vec1 + vec2) / 2;

        public static Vertex[] FacesToVertices(Face[] faces, int normalDirection)
        {
            return faces.Select(f => f.Vertices.Select(v => new Vertex(v, new Vector4(f.Normal))))
                .SelectMany(f => f)
                .ToArray();
        }
    }
}