using PersistentDataStructures.Realization.Trie;
using Xunit;

namespace TestProject1.Realization
{
    public class TrieTests
    {
        [Fact]
        public void Should_FindSingleElementOutOfOneNode()
        {
            const int insertedElement = 1;
            var trie = new PersistentTrie<int>(insertedElement);
            
            Assert.Equal(insertedElement, trie.Find(0));
        }
        
        [Fact]
        public void Should_FindElementsOutOfOneNode()
        {
            int[] insertedElements = new int[32];
            insertedElements[5] = 11;
            insertedElements[10] = 22;
            insertedElements[31] = -5;
            var trie = new PersistentTrie<int>(insertedElements);
            
            Assert.Equal(insertedElements[0], trie.Find(0));
            Assert.Equal(insertedElements[5], trie.Find(5));
            Assert.Equal(insertedElements[10], trie.Find(10));
            Assert.Equal(insertedElements[31], trie.Find(31));
        }
        
        [Fact]
        public void Should_FindElementsOutOfThreeNodes()
        {
            int[] insertedElements = new int[77];
            var trie = new PersistentTrie<int>(insertedElements);
            
            Assert.Equal(insertedElements[0], trie.Find(0));
            Assert.Equal(insertedElements[5], trie.Find(5));
            Assert.Equal(insertedElements[10], trie.Find(10));
            Assert.Equal(insertedElements[32], trie.Find(32));
            Assert.Equal(insertedElements[47], trie.Find(47));
            Assert.Equal(insertedElements[63], trie.Find(63));
            Assert.Equal(insertedElements[70], trie.Find(70));
        }
        
        [Fact]
        public void ShouldFindElementOutOfMillionElements()
        {
            int[] insertedElements = new int[10_000_001];
            insertedElements[50] = 50;
            insertedElements[100] = 100;
            insertedElements[1000] = 1000;
            insertedElements[4444] = 4444;
            insertedElements[32767] = 4444;
            insertedElements[999999] = 999999;
            insertedElements[10_000_000] = 999999;

            var trie = new PersistentTrie<int>(insertedElements);

            Assert.Equal(insertedElements[50], trie.Find(50));
            Assert.Equal(insertedElements[100], trie.Find(100));
            Assert.Equal(insertedElements[1000], trie.Find(1000));
            Assert.Equal(insertedElements[4444], trie.Find(4444));
            Assert.Equal(insertedElements[32767], trie.Find(32767));
            Assert.Equal(insertedElements[99999], trie.Find(99999));
            Assert.Equal(insertedElements[999999], trie.Find(999999));
            Assert.Equal(insertedElements[10_000_000], trie.Find(999999));
        }
    }
}