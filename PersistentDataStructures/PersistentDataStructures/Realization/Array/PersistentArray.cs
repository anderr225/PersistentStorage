using PersistentDataStructures.Interfaces;
using PersistentDataStructures.Realization.Trie;

namespace PersistentDataStructures.Realization.Array
{
    public class PersistentArray<T> : IPersistentArray<T>
    {
        private readonly IPersistentTrie<T> _trie;

        public PersistentArray(params T[] initialValues)
        {
            _trie = new PersistentTrie<T>(initialValues);
        }

        private PersistentArray(IPersistentTrie<T> trie)
        {
            _trie = trie;
        }

        public T Lookup(int index)
        {
            return _trie.Lookup(index);
        }

        public IPersistentArray<T> Update(int index, T value)
        {
            var updatedTrie = _trie.UpdateOrAdd(index, value);
            return new PersistentArray<T>(updatedTrie);
        }

        public T this[int index] => Lookup(index);
    }
}