namespace OpenGLPrimitives.Primitives.TwoD
{
    public class Circle : Entity
    {
        public Circle(float radius)
        {
            Faces = Geometry.Geometry.CreateCircle(radius, 0, false);
        }
    }
}