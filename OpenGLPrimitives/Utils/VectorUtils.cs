using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Utils
{
    public static class VectorUtils
    {
        public static Vector3 CreateRotationVector(float angleX, float angleY, float angleZ) =>
            new Vector3(MathHelper.DegreesToRadians(angleX), MathHelper.DegreesToRadians(angleY), MathHelper.DegreesToRadians(angleZ));
    }
}