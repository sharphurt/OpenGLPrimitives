using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Primitives;
using OpenTK;

namespace OpenGLPrimitives
{
    public abstract class Object
    {
        public Vector4 Position { get; }
        public Vector3 Rotation { get; }
        public Vector3 Scale { get; }

        protected Object(Vector4 position, Vector3 rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        protected Matrix4 CreateModelMatrix()
        {
            var t2 = Matrix4.CreateTranslation(Position.X, Position.Y, Position.Z);
            var r1 = Matrix4.CreateRotationX(Rotation.X);
            var r2 = Matrix4.CreateRotationY(Rotation.Y);
            var r3 = Matrix4.CreateRotationZ(Rotation.Z);
            var s = Matrix4.CreateScale(Scale);

            return s * r1 * r2 * r3 * t2;
        }

        public virtual void Render(ICamera camera, LightSource light, Shader shader)
        {
            var modelMatrix = CreateModelMatrix();
            shader.SetMat4("model", modelMatrix);
            shader.SetMat4("view", camera.LookAtMatrix);
            shader.SetVec4("lightPos", light.Position);
            shader.SetVec4("lightColor", Vector4.One);
        }
    }
}