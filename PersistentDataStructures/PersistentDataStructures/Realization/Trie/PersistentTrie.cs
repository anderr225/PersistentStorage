using PersistentDataStructures.Interfaces;

namespace PersistentDataStructures.Realization.Trie
{
    public class PersistentTrie<TValue> : IPersistentTrie<TValue>
    {
        public PersistentTrie(params TValue[] values)
        {
        }

        public TValue Lookup(int index)
        {
            throw new System.NotImplementedException();
        }

        public IPersistentTrie<TValue> UpdateOrAdd(int index, TValue value)
        {
            throw new System.NotImplementedException();
        }

        public IPersistentTrie<TValue> Remove(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}