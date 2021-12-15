using System;
using System.Collections.Generic;
using OpenGLPrimitives.Primitives;
using OpenGLPrimitives.Primitives.ThreeD;
using OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron;
using OpenGLPrimitives.Primitives.TwoD;
using OpenGLPrimitives.Utils;
using OpenTK;

namespace OpenGLPrimitives
{
    public static class GameObjectFactory
    {
        public static GameObject CreatePyramid(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Pyramid(), pos, rot, scale);

        public static GameObject CreatePlane(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Plane(), pos, rot, scale);

        public static GameObject CreateTrapezoid(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Trapezoid(), pos, rot, scale);

        public static GameObject CreateCube(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Cube(), pos, rot, scale);

        public static GameObject CreateTorus(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Torus(), pos, rot, scale);

        public static GameObject CreateSphere(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Sphere(30, 30), pos, rot, scale);

        public static GameObject CreateTetrahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Tetrahedron(), pos, rot, scale);

        public static GameObject CreateOctahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Octahedron(), pos, rot, scale);

        public static GameObject CreateDodecahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Dodecahedron(), pos, rot, scale * 0.5f);

        public static GameObject CreateIcosahedron(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Icosahedron(), pos, rot, scale);

        public static GameObject CreateCylinder(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Cylinder(0.5f, 0.5f, 1), pos, rot + VectorUtils.CreateRotationVector(-90,0,0), scale);

        public static GameObject CreateConus(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new Cylinder(0.5f, 0.001f, 1), pos, rot + VectorUtils.CreateRotationVector(-90,0,0), scale);

        public static ComplexGameObject CreateSpiral(Vector4 pos, Vector3 rot, Vector3 scale)
        {
            var spheres = new List<GameObject>();
            var angle = 0f;
            const float step = 0.05f;
            const float radius = 0.5f;

            for (var i = 0; i < 200; i++)
            {
                var x = (float) (radius * Math.Cos(angle));
                var z = (float) (radius * Math.Sin(angle));
                angle += step;
                spheres.Add(new GameObject(new Sphere(4, 4), pos + new Vector4(x, i * 0.01f, z, 1), rot,
                    scale * 0.05f));
            }

            return new ComplexGameObject(spheres, pos, rot, scale);
        }

        public static GameObject CreateRegularPolygon(int verticesCount, Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new RegularPolygon(verticesCount), pos, rot, scale);

        public static GameObject CreatePolygon(Vector4[] vertices, Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new SimplePolygon(vertices), pos, rot, scale);
        
        public static GameObject CreateCircle(Vector4 pos, Vector3 rot, Vector3 scale) =>
            new GameObject(new RegularPolygon(50), pos, rot, scale);
    }
}