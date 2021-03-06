using System;
using PersistentDataStructures.Interfaces;

namespace PersistentDataStructures.Realization.Trie
{
    public class PersistentTrie<T> : IPersistentTrie<T>
    {
        private const int Power = 5;
        private const int BranchFactor = 1 << Power;
        private const int Mask = BranchFactor - 1;

        private TrieNode<T> _rootNode;

        public int Length { get; private set; }

        public int Depth
        {
            get
            {
                switch (Length)
                {
                    case 0: return 0;
                    case 1: return 1;
                    default: return (int) Math.Ceiling(Math.Log(Length, BranchFactor));
                }
            }
        }

        public PersistentTrie()
        {
            _rootNode = new TrieNode<T>(BranchFactor);
            Length = 0;
        }

        public PersistentTrie(params T[] values)
        {
            // TODO rewrite this
            IPersistentTrie<T> tmpTrie = new PersistentTrie<T>();
            foreach (var value in values)
            {
                tmpTrie = tmpTrie.Add(value);
            }

            _rootNode = (tmpTrie as PersistentTrie<T>)?._rootNode;
            Length = values.Length;
        }

        public T Find(int index)
        {
            var currentNode = _rootNode;

            for (var shift = Power * (Depth - 1); shift != 0; shift -= Power)
            {
                var currentNodeIndex = (index >> shift) & Mask;
                currentNode = currentNode[currentNodeIndex];
                if (currentNode == null)
                {
                    throw new IndexOutOfRangeException($"No element at index {index}");
                }
            }

            return currentNode != null && currentNode[index & Mask].HasValue
                ? currentNode[index & Mask].Value
                : throw new IndexOutOfRangeException($"No element at index {index}");
        }

        public IPersistentTrie<T> Update(int index, T value)
        {
            return Update(index, value, addNewNodesIfNeeded: false);
        }

        private IPersistentTrie<T> Update(int index, T value, bool addNewNodesIfNeeded)
        {
            var updatedTrie = new PersistentTrie<T>
            {
                Length = Length,
                _rootNode = _rootNode.Clone() as TrieNode<T>
            };

            var updatedTrieNode = updatedTrie._rootNode;

            for (var shift = Power * (Depth - 1); shift > 0; shift -= Power)
            {
                var nextNodeIndex = (index >> shift) & Mask;

                updatedTrieNode[nextNodeIndex] = updatedTrieNode[nextNodeIndex]?.Clone() as TrieNode<T>;
                if (updatedTrieNode[nextNodeIndex] == null)
                {
                    if (!addNewNodesIfNeeded)
                    {
                        throw new IndexOutOfRangeException($"No element at index {index}");
                    }

                    updatedTrieNode[nextNodeIndex] = new TrieNode<T>(BranchFactor);
                }

                updatedTrieNode = updatedTrieNode[nextNodeIndex];
            }

            if (updatedTrieNode[index & Mask] == null || !updatedTrieNode[index & Mask].HasValue)
            {
                updatedTrie.Length += 1;
            }

            updatedTrieNode[index & Mask] = new TrieNode<T>(BranchFactor, value);

            return updatedTrie;
        }

        public IPersistentTrie<T> Add(T value)
        {
            if (Length == 0)
            {
                return new PersistentTrie<T>
                {
                    _rootNode = new TrieNode<T>(BranchFactor) {[0] = new TrieNode<T>(BranchFactor, value)},
                    Length = 1
                };
            }
            
            // Three different scenarios 
            // Value can be put in the last node
            if (IsThereRoomInLastNode())
            {
                return Update(Length, value);
            }
            
            // Value can be put in a new node, but depth is the same
            if (IsThereRoomInTrie())
            {
                return Update(Length, value, addNewNodesIfNeeded: true);
            }

            // Current trie is full, we need to increase the depth
            var newTrie = new PersistentTrie<T>
            {
                Length = Length + 1,
                _rootNode = new TrieNode<T>(BranchFactor)
                {
                    Nodes = {[0] = _rootNode}
                }
            };
            // TODO rewrite this, hack with length is bad
            var tmp = newTrie.Update(Length, value, addNewNodesIfNeeded: true) as PersistentTrie<T>;
            tmp.Length -= 1;
            return tmp;
        }

        public IPersistentTrie<T> Pop()
        {
            throw new System.NotImplementedException();
        }


        private bool IsThereRoomInLastNode()
        {
            return (Length % BranchFactor != 0) && IsThereRoomInTrie();
        }

        private bool IsThereRoomInTrie()
        {
            // There is no room in the trie if length is the power of a branching factor
            if (Length == 1)
            {
                return BranchFactor > 1;
            }

            var floatPower = Math.Log(Length, BranchFactor);
            return floatPower != (int)floatPower;
        }
    }
}