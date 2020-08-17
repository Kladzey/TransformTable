using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public interface ITableLoader
    {
        Table Load(TextReader textReader);
    }
}