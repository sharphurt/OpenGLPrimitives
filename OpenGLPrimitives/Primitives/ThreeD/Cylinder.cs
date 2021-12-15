using OpenGLPrimitives.Geometry;

namespace OpenGLPrimitives.Primitives.ThreeD
{
    public class Cylinder : Entity
    {
        public Cylinder(float radius1, float radius2, float height)
        {
            Faces = Geometry.Geometry.CreateCylinder(radius1, radius2, height);
        }
    }
}