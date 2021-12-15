using System.Collections.Generic;
using OpenGLPrimitives.Camera;
using OpenTK;

namespace OpenGLPrimitives
{
    public class ComplexGameObject : Object
    {
        private readonly List<GameObject> Parts;

        public ComplexGameObject(List<GameObject> parts, Vector4 position, Vector3 rotation, Vector3 scale) : base(
            position, rotation, scale)
        {
            Parts = parts;
        }

        public override void Render(ICamera camera, LightSource light, Shader shader)
        {
            foreach (var part in Parts) part.Render(camera, light, shader);
        }
    }
}