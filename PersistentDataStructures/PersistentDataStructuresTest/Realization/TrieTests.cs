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
            int[] insertedElements = new int[1_000_000];
            insertedElements[50] = 50;
            insertedElements[100] = 100;
            insertedElements[1000] = 1000;
            insertedElements[4444] = 4444;
            insertedElements[32767] = 4444;
            insertedElements[999999] = 999999;

            var trie = new PersistentTrie<int>(insertedElements);

            Assert.Equal(insertedElements[50], trie.Find(50));
            Assert.Equal(insertedElements[100], trie.Find(100));
            Assert.Equal(insertedElements[1000], trie.Find(1000));
            Assert.Equal(insertedElements[4444], trie.Find(4444));
            Assert.Equal(insertedElements[32767], trie.Find(32767));
            Assert.Equal(insertedElements[99999], trie.Find(99999));
            Assert.Equal(insertedElements[999999], trie.Find(999999));
        }
        
        [Fact]
        public void Should_UpdateElementWithoutTouchingLastVersion()
        {
            int[] insertedElements = new int[1_000_000];
            insertedElements[50] = 50;
            insertedElements[100] = 100;
            insertedElements[999999] = 999999;

            var trie = new PersistentTrie<int>(insertedElements);
            var updatedTrie = trie.Update(999999, -51);

            Assert.Equal(insertedElements[50], trie.Find(50));
            Assert.Equal(insertedElements[50], updatedTrie.Find(50));
            Assert.Equal(insertedElements[100], trie.Find(100));
            Assert.Equal(insertedElements[100], updatedTrie.Find(100));
            Assert.Equal(insertedElements[999999], trie.Find(999999));
            Assert.Equal(-51, updatedTrie.Find(999999));
        }

        [Fact]
        public void Should_UpdateSeveralTimes()
        {
            var firstTrie = new PersistentTrie<int>(0, 1);
            var secondTrie = firstTrie.Update(0, 2);
            var thirdTrie = secondTrie.Update(0, 3);

            Assert.Equal(0, firstTrie.Find(0));
            Assert.Equal(2, secondTrie.Find(0));
            Assert.Equal(3, thirdTrie.Find(0));
            
            Assert.Equal(1, firstTrie.Find(1));
            Assert.Equal(1, secondTrie.Find(1));
            Assert.Equal(1, thirdTrie.Find(1));
        }
        
        [Fact]
        public void Should_AddFirstElement()
        {
            var trie = new PersistentTrie<int>();
            var secondTrie = trie.Add(22);

            Assert.Equal(0, trie.Length);
            Assert.Equal(1, secondTrie.Length);
            Assert.Equal(22, secondTrie.Find(0));
        }
        
        [Fact]
        public void Should_IncreaseDepthWithAdd()
        {
            var trie = new PersistentTrie<int>(new int[32]);
            var secondTrie = trie.Add(22);

            Assert.Equal(1, trie.Depth);
            Assert.Equal(2, secondTrie.Depth);
            Assert.Equal(22, secondTrie.Find(32));
        }
    }
}