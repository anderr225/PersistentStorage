using System;

namespace PersistentDataStructures.Realization.Trie
{
    internal class TrieNode<T> : ICloneable
    {
        private readonly int _branchFactor;
        public TrieNode<T>[] Nodes { get; set; }

        public T Value { get; set; }

        public bool HasValue { get; set; }

        public TrieNode(int branchFactor)
        {
            _branchFactor = branchFactor;
            Nodes = new TrieNode<T>[branchFactor];
            HasValue = false;
        }
        
        public TrieNode(int branchFactor, T value)
        {
            _branchFactor = branchFactor;
            Nodes = null;
            Value = value;
            HasValue = true;
        }

        public TrieNode<T> this[int index]
        {
            get => Nodes[index];
            set => Nodes[index] = value;
        }

        public object Clone()
        {
            return new TrieNode<T>(_branchFactor, Value)
            {
                Nodes = Nodes
            };
        }
    }
}