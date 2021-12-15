using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace OpenGLPrimitives.Geometry
{
    public static class Geometry
    {
        public static Face[] CreateSphere(int UResolution, int VResolution)
        {
            var faces = new List<Face>();

            var stepU = ((float) Math.PI * 2) / UResolution;
            var stepV = ((float) Math.PI) / VResolution;
            
            for (var i = 0; i < UResolution; i++)
            {
                for (var j = 0; j < VResolution; j++)
                {
                    var u = i * stepU;
                    var v = j * stepV;
                    var un = (i + 1 == UResolution) ? (float) Math.PI * 2 : (i + 1) * stepU;
                    var vn = (j + 1 == VResolution) ? (float) Math.PI : (j + 1) * stepV;

                    var p0 = Sphere(u, v, 1);
                    var p1 = Sphere(u, vn, 1);
                    var p2 = Sphere(un, v, 1);
                    var p3 = Sphere(un, vn, 1);

                    faces.Add(new Face(p0, p2, p1));
                    faces.Add(new Face(p3, p1, p2));
                }
            }

            return faces.ToArray();
        }

        private static Vector4 Sphere(float u, float v, float r) =>
            new Vector4((float) (Math.Cos(u) * Math.Sin(v) * r),
                (float) (Math.Cos(v) * r),
                (float) (Math.Sin(u) * Math.Sin(v) * r), 1);


        public static Face[] CreateTorus(int numc, int numt)
        {
            const float twopi = 2 * (float) Math.PI;

            var faces = new List<Face>();
            for (var i = 0; i < numc; i++)
            {
                var vertices = new List<Vector4>();
                for (var j = numt; j >= 0; j--)
                {
                    for (var k = 1; k >= 0; k--)
                    {
                        var s = (float) ((i + k) % numc + 0.5);
                        float t = j % numt;

                        var x = (float) ((0.5 + .1 * Math.Cos(s * twopi / numc)) * Math.Cos(t * twopi / numt));
                        var y = (float) ((0.5 + .1 * Math.Cos(s * twopi / numc)) * Math.Sin(t * twopi / numt));
                        var z = (float) (.1 * Math.Sin(s * twopi / numc));
                        vertices.Add(new Vector4(x, z, y, 1));
                    }
                }

                faces.Add(new Face(vertices.ToArray()));
            }

            return faces.ToArray();
        }

        private static Face[] CreateCircle(float radius, float zValue, bool reverseOrder)
        {
            var angle = 0f;
            var angle_stepsize = 0.1f;
            var vertices = new List<Vector4>();
            while (angle < 2 * Math.PI)
            {
                var x = (float) (radius * Math.Cos(angle));
                var y = (float) (radius * Math.Sin(angle));
                vertices.Add(new Vector4(x, y, zValue, 1));
                angle += angle_stepsize;
            }

            vertices.Add(new Vector4(radius, 0, zValue, 1));
            return new[] {new Face(reverseOrder ? vertices.ToArray().Reverse().ToArray() : vertices.ToArray())};
        }

        public static Face[] CreateCylinder(float radius1, float radius2, float height)
        {
            const float angleStepsize = 0.1f;

            var faces = new List<Face>();

            var angle = 0f;
            while (angle < 2 * Math.PI)
            {
                var x1 = (float) (Math.Cos(angle));
                var y1 = (float) (Math.Sin(angle));
                var x2 = (float) (Math.Cos(angle + angleStepsize));
                var y2 = (float) (Math.Sin(angle + angleStepsize));

                var face = new Face(
                    new Vector4(x2 * radius1, y2 * radius1, 0, 1),
                    new Vector4(x2 * radius2, y2 * radius2, height, 1),
                    new Vector4(x1 * radius2, y1 * radius2, height, 1),
                    new Vector4(x1 * radius1, y1 * radius1, 0, 1));

                faces.Add(face);
                angle += angleStepsize;
            }

            faces.AddRange(new[]
            {
                CreateCircle(radius1, 0, true).First(),
                CreateCircle(radius2, height, false).First()
            });

            return faces.ToArray();
        }

        public static Face CreateRegularPolygon(float radius, int verticesCount)
        {
            var tempX = -radius / 2;
            var tempY = -radius / 2;

            var originX = radius / 2;
            var originY = radius / 2;

            var points = new List<Vector4>();

            for (var i = 0; i < verticesCount; i++)
            {
                var angle = 360 / verticesCount * i * 0.0174533;
                var rX = (float) (originX + tempX * Math.Cos(angle) - tempY * Math.Sin(angle));
                var rY = (float) (originY + tempX * Math.Sin(angle) + tempY * Math.Cos(angle));

                points.Add(new Vector4(rX, rY, 0, 1));
            }

            return new Face(points.ToArray());
        }
    }
}