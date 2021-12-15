using System.Collections.Generic;
using System.Linq;
using OpenGLPrimitives.Geometry;
using OpenTK;

namespace OpenGLPrimitives.Primitives.ThreeD.RegularPolyhedron
{
    public class Cube : Entity
    {
        private readonly Vector4[] _points =
        {
            new Vector4(0.5f, 0.5f, 0.5f, 1),
            new Vector4(-0.5f, 0.5f, 0.5f, 1),
            new Vector4(0.5f, -0.5f, 0.5f, 1),
            new Vector4(-0.5f, -0.5f, 0.5f, 1),

            new Vector4(0.5f, 0.5f, -0.5f, 1),
            new Vector4(-0.5f, 0.5f, -0.5f, 1),
            new Vector4(0.5f, -0.5f, -0.5f, 1),
            new Vector4(-0.5f, -0.5f, -0.5f, 1),
        };

        private readonly IEnumerable<int>[] _indices =
        {
            new[] {0, 1, 3, 2}.Reverse(),
            new[] {4, 5, 7, 6}.Reverse(),
            new[] {0, 1, 5, 4}.Reverse(),
            new[] {2, 3, 7, 6}.Reverse(),
            new[] {0, 2, 6, 4}.Reverse(),
            new[] {1, 3, 7, 5}.Reverse(),
        };

        public Cube()
        {
            Faces = _indices.Select(f => new Face(f.Select(i => _points[i]).ToArray())).ToArray();
        }
    }
}