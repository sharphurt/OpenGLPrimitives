using System;
using System.Linq;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Primitives;
using OpenTK;

namespace OpenGLPrimitives
{
    public class GameObject
    {
        public Entity Entity => _entity;
        public Vector4 Position => _position;
        public Vector3 Scale => _scale;

        private Entity _entity;
        private Vector4 _position;
        private Vector3 _rotation;
        private Matrix4 _modelMatrix;
        private Vector3 _scale;


        public GameObject(Entity entity, Vector4 position, Vector3 rotation, Vector3 scale)
        {
            _entity = entity;
            _position = position;
            _rotation = rotation;
            _scale = scale;
        }

        public virtual void Render(ICamera camera, LightSource light, Shader shader)
        {
            var t2 = Matrix4.CreateTranslation(_position.X, _position.Y, _position.Z);

            var r1 = Matrix4.CreateRotationX(_rotation.X);
            var r2 = Matrix4.CreateRotationY(_rotation.Y);
            var r3 = Matrix4.CreateRotationZ(_rotation.Z);
            var s = Matrix4.CreateScale(_scale);

            _modelMatrix = s * r1 * r2 * r3 * t2;

            shader.SetMat4("model", _modelMatrix);
            shader.SetMat4("view", camera.LookAtMatrix);
            shader.SetVec4("lightPos", light.Position);
            shader.SetVec4("lightColor", Vector4.One);
            foreach (var polygon in _entity.Polygons)
            {
                polygon.Bind();
                polygon.Render();
            }
        }
    }
}