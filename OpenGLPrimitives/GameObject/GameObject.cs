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
            base.Render(camera, light, shader);
            foreach (var polygon in Entity.Polygons)
            {
                polygon.Bind();
                polygon.Render();
            }
        }
    }
}