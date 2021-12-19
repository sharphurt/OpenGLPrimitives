using OpenTK;

namespace OpenGLPrimitives.Camera
{
    public interface ICamera
    {
        Matrix4 LookAtMatrix { get; }
        
        float MoveSpeed { get; set; }
        
        Vector3 Position { get; }

        Vector3 Direction { get; }
        
        Vector3 Up { get; }
        
        void Move(float dx, float dy, float dz);

        void AddRotation(float dx, float dz);
    }
}