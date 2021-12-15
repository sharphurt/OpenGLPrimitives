using System.Drawing;

namespace OpenGLPrimitives
{
    public class MtlData
    {
        public string Name { get; }
        
        public string TexturePath { get; }

        public MtlData(string name, string texture)
        {
            Name = name;
            TexturePath = texture;
        }
    }
}