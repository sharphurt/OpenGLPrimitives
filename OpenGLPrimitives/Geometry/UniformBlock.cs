using System.Numerics;
using OpenTK;

namespace OpenGLPrimitives.Geometry
{
    struct UniformBlock
    {
        public const int Size = (4 * 4) * 3 * sizeof(float);
        
        public Matrix4 ModelViewMatrix;
        public Matrix4 ViewMatrix;
        public Matrix4 ProjectionMatrix;

        public UniformBlock(Matrix4 modelViewMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            ModelViewMatrix = modelViewMatrix;
            ViewMatrix = viewMatrix;
            ProjectionMatrix = projectionMatrix;
        }
    };
}