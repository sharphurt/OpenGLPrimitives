namespace OpenGLPrimitives.Primitives
{
    public class RegularPolygon : Entity
    {
        public RegularPolygon(int verticesCount)
        {
            Faces = new[] {Geometry.Geometry.CreateRegularPolygon(1, verticesCount)};
        }
    }
}