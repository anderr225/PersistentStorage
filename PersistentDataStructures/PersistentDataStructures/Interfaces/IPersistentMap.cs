namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentMap<TKey, TValue>
    {
        TValue Find(TKey key);

        IPersistentMap<TKey, TValue> Update(TKey key, TValue value);

        TValue this[TKey key] { get; set; }
    }
}