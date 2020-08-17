namespace Kladzey.TransformTable.Logic
{
    public interface IFactory<T>
    {
        Owned<T> Create();
    }
}