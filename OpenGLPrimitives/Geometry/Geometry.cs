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

                    faces[i].A = pa;
                    faces[i].B = pb;
                    faces[i].C = pc;
                }
            }

            return faces;
        }

        public static List<Vertex> CreateSphere(int lats, int longs)
        {
            var vertices = new List<Vertex>();
            int i, j;
            for (i = 0; i <= lats; i++)
            {
                float lat0 = (float) (Math.PI * (-0.5 + (double) (i - 1) / lats));
                float z0 = (float) Math.Sin(lat0);
                float zr0 = (float) Math.Cos(lat0);

                float lat1 = (float) (Math.PI * (-0.5 + (float) i / lats));
                float z1 = (float) Math.Sin(lat1);
                float zr1 = (float) Math.Cos(lat1);

                for (j = 0; j <= longs; j++)
                {
                    float lng = MathHelper.TwoPi * (j - 1) / longs;
                    float x = (float) Math.Cos(lng);
                    float y = (float) Math.Sin(lng);

                    vertices.Add(new Vertex(new Vector4(x * zr0, y * zr0, z0, 1),
                        new Vector4(x * zr0, y * zr0, z0, 1)));
                    vertices.Add(new Vertex(new Vector4(x * zr1, y * zr1, z1, 1),
                        new Vector4(x * zr1, y * zr1, z1, 1)));
                }
            }

            return vertices;
        }

        public static Vertex[] CreateTorus(float radius)
        {
            const int numc = 100;
            const int numt = 100;

            var result = new List<Vertex>();

            //Gl.glBegin(Gl.GL_QUAD_STRIP);

            for (var i = 0; i < numc; i++)
            for (var j = 0; j <= numt; j++)
            for (var k = 0; k <= 1; k++)
            {
                var s = (float) (i + k) % numc + 0.5;
                var t = j % numt;

                var x = (float) ((radius + 0.1 * Math.Cos(s * MathHelper.TwoPi / numc)) *
                                 Math.Cos(t * MathHelper.TwoPi / numt));
                var y = (float) ((radius + 0.1 * Math.Cos(s * MathHelper.TwoPi / numc)) *
                                 Math.Sin(t * MathHelper.TwoPi / numt));
                var z = (float) (0.1 * Math.Sin(s * MathHelper.TwoPi / numc));

                result.Add(new Vertex(new Vector4(2 * x, 2 * z, 2 * y, 1), new Vector4(x, z, y, 1)));
            }

            return result.ToArray();
        }


        public static Face[] CreateUnitSphere(int iterations)
        {
            var pa = new Vector4(1, 1, 1, 1);
            var pb = new Vector4(-1, -1, 1, 1);
            var pc = new Vector4(1, -1, -1, 1);
            var pd = new Vector4(-1, 1, -1, 1);

            var faces = new[]
            {
                new Face(pa, pb, pc),
                new Face(pb, pa, pd),
                new Face(pb, pd, pc),
                new Face(pa, pc, pd)
            }.ToList();

            var n = 4;

            for (var i = 1; i < iterations; i++)
            {
                var nstart = n;

                for (var j = 0; j < nstart; j++)
                {
                    faces.AddRange(new[]
                    {
                        faces[j],
                        faces[j],
                        faces[j]
                    });
                    var p1 = VectorUtils.MiddlePoint(faces[j].A, faces[j].B);
                    var p2 = VectorUtils.MiddlePoint(faces[j].B, faces[j].C);
                    var p3 = VectorUtils.MiddlePoint(faces[j].C, faces[j].A);

                    faces[j].B = p1;
                    faces[j].C = p3;

                    faces[n].A = p1;
                    faces[n].C = p2;

                    faces[n + 1].A = p3;
                    faces[n + 1].B = p2;

                    faces[n + 2].A = p1;
                    faces[n + 2].B = p2;

                    faces[n + 2].C = p3;
                    n += 3;
                }
            }

            return faces.ToArray();
        }
    }
}