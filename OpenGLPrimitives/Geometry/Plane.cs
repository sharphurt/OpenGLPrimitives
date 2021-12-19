using OpenTK;

namespace OpenGLPrimitives.Geometry
{
    public struct Plane
    {
        public const int Size = (3 + 1) * sizeof(float);
        
        public Vector3 Normal;
        public float D;
    }
}