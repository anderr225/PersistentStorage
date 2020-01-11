namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentMap<TKey, TValue>
    {
        TValue Lookup(TKey key);

        IPersistentMap<TKey, TValue> Update(TKey key, TValue value);

        TValue this[TKey key] { get; set; }
    }
}