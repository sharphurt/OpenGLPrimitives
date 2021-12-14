using OpenGLPrimitives.Primitives.ThreeD;
using OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron;
using OpenGLPrimitives.Primitives.TwoD;
using OpenTK;

namespace OpenGLPrimitives
{
    public static class GameObjectFactory
    {
        public static GameObject CreatePyramid(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Pyramid(), pos, rot, scale);

        public static GameObject CreatePlane(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Plane(), pos, rot, scale);
        
        public static GameObject CreateCube(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Cube(), pos, rot, scale);

        public static GameObject CreateTorus(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Torus(), pos, rot, scale);

        public static GameObject CreateSphere(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Sphere(), pos, rot, scale);

        public static GameObject CreateTetrahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Tetrahedron(), pos, rot, scale);

        public static GameObject CreateOctahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Octahedron(), pos, rot, scale);

        public static GameObject CreateDodecahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Dodecahedron(), pos, rot, scale);
        
        public static GameObject CreateIcosahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Icosahedron(), pos, rot, scale);
    }
}