using System;
using NUnit.Framework;

namespace Algorithms
{
    public class SparseTableExample
    {
        public static void TestMin()
        {
            var arr = new [] { 1, 2, 3, -1, 2, 5, 6, 3, 5, 8, 0 };
            var min = new Operation<int, int>(
                mapper: x => x,
                merger: Math.Min,
                isOverlapFriendly: true);
            SparseTable<int, int> minSparseTable = new SparseTable<int, int>(arr, min);
            Console.WriteLine(minSparseTable);
            Assert.AreEqual(-1, minSparseTable.Query(0, 3));
            Console.WriteLine($"Min element from [0, 3] is {minSparseTable.Query(0, 3)}");
            Assert.AreEqual(1, minSparseTable.Query(0, 2));
            Console.WriteLine($"Min element from [0, 2] is {minSparseTable.Query(0, 2)}");
            Assert.AreEqual(2, minSparseTable.Query(1, 2));
            Console.WriteLine($"Min element from [1, 2] is {minSparseTable.Query(1, 2)}");
        }
        
        public static void TestSum()
        {
            var arr = new [] { 1, 2, 3, -1, 2, 5, 6, 3, 5, 8, 0 };
            var sum = new Operation<int, long>(
                mapper: x => x,
                merger: (a, b) => a + b,
                isOverlapFriendly: false);
            SparseTable<int, long> sumSparseTable = new SparseTable<int, long>(arr, sum);
            Console.WriteLine(sumSparseTable);
            Assert.AreEqual(5, sumSparseTable.Query(0, 3));
            Console.WriteLine($"Sum of elements from [0, 3] is {sumSparseTable.Query(0, 3)}");
            Assert.AreEqual(6, sumSparseTable.Query(0, 2));
            Console.WriteLine($"Sum of elements from [0, 2] is {sumSparseTable.Query(0, 2)}");
            Assert.AreEqual(5, sumSparseTable.Query(1, 2));
            Console.WriteLine($"Sum of elements from [1, 2] is {sumSparseTable.Query(1, 2)}");
        }
    }
}