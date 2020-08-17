namespace Kladzey.TransformTable.Logic
{
    public interface ITableTransformer
    {
        Table Transform(Table inputTable, string query);
    }
}