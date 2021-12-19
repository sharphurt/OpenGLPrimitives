namespace OpenGLPrimitives.Geometry
{
    struct Ray
    {
        public int RayOrigin;
        public int RayLookat;
        public int Aspect;

        public Ray(int rayOrigin, int rayLookat, int aspect)
        {
            RayOrigin = rayOrigin;
            RayLookat = rayLookat;
            Aspect = aspect;
        }
    }
}