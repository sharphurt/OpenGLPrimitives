using System;
using System.Collections.Generic;
using OpenGLPrimitives.Camera;
using OpenGLPrimitives.Geometry;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives
{
    public class ObjGameObject : Object
    {
        public List<Mesh> Meshes { get; }

        public ObjGameObject(List<Mesh> meshes, Vector4 position, Vector3 rotation, Vector3 scale) : base(position, rotation, scale)
        {
            Meshes = meshes;
        }

        public override void Render(ICamera camera, LightSource light, Shader shader)
        {
            base.Render(camera, light, shader);
            foreach (var mesh in Meshes)
            {
                mesh.Bind();
                mesh.Render();
            }
        }
    }
}