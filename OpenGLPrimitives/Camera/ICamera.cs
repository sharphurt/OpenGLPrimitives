using OpenTK;

namespace OpenGLPrimitives.Camera
{
    public interface ICamera
    {
        Matrix4 LookAtMatrix { get; }
        
        float MoveSpeed { get; set; }

        void Move(float dx, float dy, float dz);

        void AddRotation(float dx, float dz);
    }
}