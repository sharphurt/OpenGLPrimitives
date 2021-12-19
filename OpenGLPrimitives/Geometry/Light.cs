using OpenTK;

namespace OpenGLPrimitives.Geometry
{
    public struct Light
    {
        public const int Size = 3 * sizeof(float);
        
        public Vector3 Position;

        public Light(Vector3 position)
        {
            Position = position;
        }
    }
}