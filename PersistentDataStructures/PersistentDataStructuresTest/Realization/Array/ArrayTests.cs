using Moq;
using PersistentDataStructures.Interfaces;
using PersistentDataStructures.Realization.Array;
using PersistentDataStructures.Realization.Trie;
using Xunit;

namespace TestProject1.Realization.Array
{
    public class ArrayTests
    {
        private readonly Mock<IPersistentTrie<int>> _trieMock = new Mock<IPersistentTrie<int>>();
        private readonly PersistentArray<int> _target;

        public ArrayTests()
        {
            _target = new PersistentArray<int>(_trieMock.Object);
        }

        [Fact]
        public void Should_CallFindOnTrie()
        {
            _trieMock.Setup(x => x.Find(10)).Returns(11);
            _trieMock.Setup(x => x.Find(20)).Returns(22);

            var foundByMethod = _target.Find(10);
            var foundByIndexing = _target[20];

            Assert.Equal(11, foundByMethod);
            Assert.Equal(22, foundByIndexing);

            _trieMock.Verify(x => x.Find(10), Times.Once);
            _trieMock.Verify(x => x.Find(20), Times.Once);
        }

        [Fact]
        public void Should_CallUpdateOnTrie()
        {
            _trieMock.Setup(x => x.Update(10, 20)).Returns(new PersistentTrie<int>());

            var updated = _target.Update(10, 20);

            Assert.NotNull(updated);

            _trieMock.Verify(x => x.Update(10, 20), Times.Once);
        }

        [Fact]
        public void Should_UpdateElementWithoutTouchingLastVersion()
        {
            int[] insertedElements = new int[1_000_000];
            insertedElements[50] = 50;
            insertedElements[100] = 100;
            insertedElements[999999] = 999999;

            var array = new PersistentArray<int>(insertedElements);
            var updatedArray = array.Update(999999, -51);

            Assert.Equal(insertedElements[50], array[50]);
            Assert.Equal(insertedElements[50], updatedArray[50]);
            Assert.Equal(insertedElements[100], array[100]);
            Assert.Equal(insertedElements[100], updatedArray[100]);
            Assert.Equal(insertedElements[999999], array[999999]);
            Assert.Equal(-51, updatedArray[999999]);
        }

        [Fact]
        public void Should_UpdateSeveralTimes()
        {
            var firstArray = new PersistentArray<int>(0, 1);
            var secondArray = firstArray.Update(0, 2);
            var thirdArray = secondArray.Update(0, 3);

            Assert.Equal(0, firstArray[0]);
            Assert.Equal(2, secondArray[0]);
            Assert.Equal(3, thirdArray[0]);

            Assert.Equal(1, firstArray[1]);
            Assert.Equal(1, secondArray[1]);
            Assert.Equal(1, thirdArray[1]);
        }
    }
}