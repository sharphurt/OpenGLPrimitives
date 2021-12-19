using OpenTK;

namespace OpenGLPrimitives.Geometry
{
    public struct Sphere
    {
        public const int Size = (3 + 4 + 1) * sizeof(float);
        
        public Vector3 Center;
        public float Radius;
        public Vector4 Color;

        public Sphere(Vector3 center, float radius, Vector4 color)
        {
            Center = center;
            Radius = radius;
            Color = color;
        }
    }
}