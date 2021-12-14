using System;
using System.Drawing;
using OpenTK;

namespace OpenGLPrimitives
{
    public static class ColorUtils
    {
        private static readonly Random random = new Random();

        public static Vector4 GenerateRandomColor()
        {
            return new Vector4(
                (float) random.NextDouble(),
                (float) random.NextDouble(),
                (float) random.NextDouble(),
                (float) random.NextDouble());
        }
    }
}