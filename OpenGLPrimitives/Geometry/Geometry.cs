using System;
using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Utils;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenGLPrimitives.Geometry
{
    public static class Geometry
    {
        public static List<Face> CreateRegularPolyhedron(int iterations)
        {
            var basic = new[]
            {
                new Vector4(0, 0, 1, 1),
                new Vector4(0, 0, -1, 1),
                new Vector4(-1, -1, 0, 1),
                new Vector4(1, -1, 0, 1),
                new Vector4(1, 1, 0, 1),
                new Vector4(-1, 1, 0, 1)
            };

            var a = (float) (1 / Math.Sqrt(2.0));

            for (var i = 0; i < 6; i++)
            {
                basic[i].X *= a;
                basic[i].Y *= a;
            }

            var faces = new List<Face>
            {
                new Face(basic[0], basic[3], basic[4]),
                new Face(basic[0], basic[4], basic[5]),
                new Face(basic[0], basic[5], basic[2]),
                new Face(basic[0], basic[2], basic[3]),
                new Face(basic[1], basic[4], basic[3]),
                new Face(basic[1], basic[5], basic[4]),
                new Face(basic[1], basic[2], basic[5]),
                new Face(basic[1], basic[3], basic[2])
            };

            var nt = 8;

            if (iterations < 1)
                return faces;

            for (var it = 0; it < iterations; it++)
            {
                var ntold = nt;
                for (var i = 0; i < ntold; i++)
                {
                    var pa = VectorUtils.MiddlePoint(faces[i].A, faces[i].B).Normalized();
                    var pb = VectorUtils.MiddlePoint(faces[i].B, faces[i].C).Normalized();
                    var pc = VectorUtils.MiddlePoint(faces[i].C, faces[i].A).Normalized();

                    faces.Add(new Face(faces[i].A, pa, pc));
                    faces.Add(new Face(pa, faces[i].B, pb));
                    faces.Add(new Face(pb, faces[i].C, pc));

                    nt += 3;

                    faces[i] = new Face(pa, pb, pc);
                }
            }

            return faces;
        }

        public static Face[] CreateSphere(int UResolution, int VResolution)
        {
            var startU = 0;
            var startV = 0;
            var endU = (float) Math.PI * 2;
            var endV = (float) Math.PI;
            var stepU = (endU - startU) / UResolution;

            var faces = new List<Face>();

            var stepV = (endV - startV) / VResolution;
            for (var i = 0; i < UResolution; i++)
            {
                // U-points
                for (var j = 0; j < VResolution; j++)
                {
                    // V-points
                    var u = i * stepU + startU;
                    var v = j * stepV + startV;
                    var un = (i + 1 == UResolution) ? endU : (i + 1) * stepU + startU;
                    var vn = (j + 1 == VResolution) ? endV : (j + 1) * stepV + startV;
                    // Find the four points of the grid
                    // square by evaluating the parametric
                    // surface function
                    var p0 = Sphere(u, v, 1);
                    var p1 = Sphere(u, vn, 1);
                    var p2 = Sphere(un, v, 1);
                    var p3 = Sphere(un, vn, 1);
                    // NOTE: For spheres, the normal is just the normalized
                    // version of each vertex point; this generally won't be the case for
                    // other parametric surfaces.
                    // Output the first triangle of this grid square
                    faces.Add(new Face(p0, p2, p1));
                    // Output the other triangle of this grid square
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

        
        
    }
}