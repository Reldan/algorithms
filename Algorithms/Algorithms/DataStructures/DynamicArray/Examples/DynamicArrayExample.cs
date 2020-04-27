using NUnit.Framework;

namespace Algorithms.DataStructures.DynamicArray.Examples
{
    [TestFixture]
    public class DynamicArrayExample
    {
        [Test]
        public void SimpleTest()
        {
         
            DynamicArray<int> arr = new DynamicArray<int>(1);
            for (int i = 0; i < 20; i++)
            {
                arr.Add(i);
            }
            Assert.AreEqual(20, arr.Count);
            for (int i = 0; i < 20; i++)
            {
               Assert.AreEqual(i, arr[i]);
            }

            arr.RemoveAt(4);
            Assert.AreEqual(19, arr.Count);
            for (int i = 0; i < 4; i++)
            {
                Assert.AreEqual(i, arr[i]);
            }
            for (int i = 4; i < 19; i++)
            {
                Assert.AreEqual(i + 1, arr[i]);
            }
        }
    }
}