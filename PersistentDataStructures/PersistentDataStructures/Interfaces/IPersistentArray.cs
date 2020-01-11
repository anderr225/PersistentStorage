using System.Collections.Generic;

namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentArray<T>
    {
        T Lookup(int index);

        IPersistentArray<T> Update(int index, T value);

        T this[int index] { get; }
    }
}