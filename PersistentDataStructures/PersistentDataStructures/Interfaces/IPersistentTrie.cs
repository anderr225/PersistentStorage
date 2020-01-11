namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentTrie<TValue>
    {
        TValue Lookup(int index);
        
        IPersistentTrie<TValue> UpdateOrAdd(int index, TValue value);
        
        IPersistentTrie<TValue> Remove(int index);
    }
}