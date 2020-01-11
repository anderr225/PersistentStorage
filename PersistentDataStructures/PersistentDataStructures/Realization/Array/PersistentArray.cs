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

        public PersistentArray(IPersistentTrie<T> trie)
        {
            _trie = trie;
        }

        public T Find(int index)
        {
            return _trie.Find(index);
        }

        public IPersistentArray<T> Update(int index, T value)
        {
            var updatedTrie = _trie.Update(index, value);
            return new PersistentArray<T>(updatedTrie);
        }

        public T this[int index] => Find(index);
    }
}