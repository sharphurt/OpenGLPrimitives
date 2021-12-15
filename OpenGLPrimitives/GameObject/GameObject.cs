using System;
using System.Linq;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Geometry;
using OpenGLPrimitives.Primitives;
using OpenTK;

namespace OpenGLPrimitives
{
    public class GameObject : Object
    {
        public Entity Entity { get; }

        public GameObject(Entity entity, Vector4 position, Vector3 rotation, Vector3 scale) : base(position, rotation,
            scale)
        {
            Entity = entity;
        }

        public override void Render(ICamera camera, LightSource light, Shader shader)
        {
            var modelMatrix = CreateModelMatrix();
            shader.SetMat4("model", modelMatrix);
            shader.SetMat4("view", camera.LookAtMatrix);
            shader.SetVec4("lightPos", light.Position);
            shader.SetVec4("lightColor", Vector4.One);
            foreach (var polygon in Entity.Polygons)
            {
                polygon.Bind();
                polygon.Render();
            }
        }
    }
}