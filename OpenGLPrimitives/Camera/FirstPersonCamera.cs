using System;
using OpenTK;

namespace OpenGLPrimitives.Camera
{
    public class FirstPersonCamera : ICamera
    {
        public Matrix4 LookAtMatrix
        {
            get => Matrix4.LookAt(Position, Position + Direction, Vector3.UnitY);
        }

        public Vector3 Position { get; private set; } = Vector3.One;
        
        private Vector3 Orientation = new Vector3((float) Math.PI, 0f, 1f);
        
        public float MoveSpeed { get; set; }
        
        private const float MouseSensitivity = 0.0025f;

        public Vector3 Up => Vector3.Cross(Direction, Vector3.Cross(Vector3.UnitY, Direction));

        public Vector3 Direction => new Vector3
        {
            X = (float) (Math.Sin(Orientation.X) * Math.Cos(Orientation.Y)),
            Y = (float) Math.Sin(Orientation.Y),
            Z = (float) (Math.Cos(Orientation.X) * Math.Cos(Orientation.Y))
        };

        public void Move(float x, float y, float z)
        {
            Vector3 offset = new Vector3();

            Vector3 forward = new Vector3((float) Math.Sin(Orientation.X), 0, (float) Math.Cos(Orientation.X));
            Vector3 right = new Vector3(-forward.Z, 0, forward.X);

            offset += x * right;
            offset += y * forward;
            offset.Y += z;

            offset.NormalizeFast();
            offset = Vector3.Multiply(offset, MoveSpeed);

            Position += offset;
        }

        public void AddRotation(float x, float y)
        {
            x *= MouseSensitivity;
            y *= MouseSensitivity;

            Orientation.X = (Orientation.X + x) % ((float) Math.PI * 2.0f);
            Orientation.Y = Math.Max(Math.Min(Orientation.Y + y, (float) Math.PI / 2.0f - 0.1f),
                (float) -Math.PI / 2.0f + 0.1f);
        }
    }
}