using System;
using NUnit.Framework;

namespace Algorithms.DataStructures.FenwickTree.Examples
{
    [TestFixture]
    public class FenwickTreeExample
    {
        [Test]
        public void Test()
        {
            Console.WriteLine("her");
            FenwickTree ft = new FenwickTree(new [] {3, 4, -2, 7, 3, 11, 5, -8, -9, 2, 4, -8l});
            
            //  1   1
            //  3   4
            //  2   2
            // -1   4
            //  4

            Assert.AreEqual(5, ft.PrefixSum(2));
            Assert.AreEqual(8, ft.Sum(2, 4));
            Console.WriteLine(ft.ToString());
        }
    }
}