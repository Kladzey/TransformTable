using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public interface ITextTransformer
    {
        void Transform(TextReader input, TextWriter output, string query);
    }
}