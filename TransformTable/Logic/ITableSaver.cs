using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public interface ITableSaver
    {
        void Save(Table table, TextWriter textWriter);
    }
}