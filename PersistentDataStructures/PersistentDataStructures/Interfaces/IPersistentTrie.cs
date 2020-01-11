namespace PersistentDataStructures.Interfaces
{
    public interface IPersistentTrie<TValue>
    {
        TValue Find(int index);
        
        IPersistentTrie<TValue> Update(int index, TValue value);
        
        IPersistentTrie<TValue> Add(TValue value);
        
        IPersistentTrie<TValue> Pop();

        int Length { get; }
        
        int Depth { get; }
    }
}