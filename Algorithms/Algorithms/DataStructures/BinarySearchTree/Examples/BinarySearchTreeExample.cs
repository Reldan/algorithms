using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Algorithms.DataStructures.BinarySearchTree.Examples
{
    public class BinarySearchTreeExample
    {
        [Test]
        public static void Test()
        {
            BinarySearchTree<int> inOrderBst = new BinarySearchTree<int>(Traverse.InOrder);
            inOrderBst.Add(3);
            inOrderBst.Add(1);
            inOrderBst.Add(2);
            inOrderBst.Add(4);
            inOrderBst.Add(5);
            
            //          3
            //      1      4
            //        2       5
            
            Assert.AreEqual(5, inOrderBst.Count);
            using var inOrder= inOrderBst.GetEnumerator();
            CollectionAssert.AreEqual(new List<int> {1, 2, 3, 4, 5}, new List<int>(inOrderBst));
            var preOrderBst = new BinarySearchTree<int>(inOrderBst, Traverse.PreOrder);
     
            CollectionAssert.AreEqual(new List<int> {3, 1, 2, 4, 5}, new List<int>(preOrderBst));
            var postOrderBst = new BinarySearchTree<int>(inOrderBst, Traverse.PostOrder);
            
            CollectionAssert.AreEqual(new List<int> {2, 1, 5, 4, 3}, new List<int>(postOrderBst));

            var levelOrderBst = new BinarySearchTree<int>(inOrderBst, Traverse.LevelOrder);
            CollectionAssert.AreEqual(new List<int> {3, 1, 4, 2, 5}, new List<int>(levelOrderBst));

            inOrderBst.Remove(3);
            CollectionAssert.AreEqual(new List<int> {1, 2, 4, 5}, new List<int>(inOrderBst));
        }
    }
}