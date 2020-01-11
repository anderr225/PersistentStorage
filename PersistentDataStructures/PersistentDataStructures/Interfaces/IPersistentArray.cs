namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentArray<T>
    {
        T Find(int index);

        IPersistentArray<T> Update(int index, T value);

        T this[int index] { get; }
    }
}