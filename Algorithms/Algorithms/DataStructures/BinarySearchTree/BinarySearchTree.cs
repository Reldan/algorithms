using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.DataStructures.BinarySearchTree
{
    public class BinarySearchTree<T> : ICollection<T> where T : notnull
    {
        private readonly IComparer<T> _comparer;
        private int _nodeCount = 0;
        private Node _root;
        private readonly Traverse _traverse;

        private class Node
        {
            public T Data;
            public Node Left;
            public Node Right;

            public Node(Node left, Node right, T data)
            {
                Data = data;
                Left = left;
                Right = right;
            }
        }

        public BinarySearchTree(): this(Comparer<T>.Default, Traverse.InOrder)
        {
        }
        
        public BinarySearchTree(Traverse traverse) : this(Comparer<T>.Default, traverse) {}
        
        public BinarySearchTree(IComparer<T> comparer, Traverse traverse)
        {
            _comparer = comparer;
            _traverse = traverse;
            Clear();
        }

        public BinarySearchTree(BinarySearchTree<T> binarySearchTree, Traverse traverse)
        {
            _comparer = binarySearchTree._comparer;
            _traverse = traverse;
            _root = Clone(binarySearchTree._root);
            _nodeCount = binarySearchTree.Count;
        }

        private static Node Clone(Node node)
        {
            return node == null 
                ? null 
                : new Node(Clone(node.Left), Clone(node.Right), node.Data);
        }

        public void Clear()
        {
            _root = null;
            _nodeCount = 0;
        }

        public bool Contains(T item)
        {
            return Contains(_root, item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            using var enumerator = GetEnumerator();
            for (int i = arrayIndex; i < array.Length && enumerator.MoveNext(); i++)
                array[i] = enumerator.Current;
        }

        public bool Remove(T item)
        {
            if (item == null) return false;
            if (!Contains(item)) return false;
            _root = Remove(_root, item);
            _nodeCount--;
            return true;
        }

        private Node RemoveNode(Node node)
        {
            if (node == null) return null;
            if (node.Left == null)
            {
                return node.Right;
            }
            if (node.Right == null)
            {
                return node.Left;
            }
            Node minNode = FindMin(node.Right);
            node.Data = minNode.Data;
            node.Right = Remove(node.Right, minNode.Data);
            return node;
        }

        private Node FindMin(Node node)
        {
            if (node == null) return null;
            while (node.Left != null) node = node.Left;
            return node;
        }

        private Node Remove(Node node, T item)
        {
            if (node == null) return null;
            if (item == null) return node;
            int result = _comparer.Compare(node.Data, item);
            if (result == 0)
            {
                node = RemoveNode(node);
            }
            else if (result < 0)
            {
                node.Left = Remove(node, item);
            }
            else
            {
                node.Right = Remove(node, item);
            }

            return node;
        }

        public void Add(T item)
        {
            if (item == null) return;
            if (Contains(item)) return;
            _root = Add(_root, item);
            _nodeCount++;
        }

        private Node Add(Node node, T item)
        {
            if (item == null) return node;
            if (node == null) return new Node(null, null, item);
            int result = _comparer.Compare(node.Data, item);
            if (result == 0) return node;
            if (result > 0)
            {
                node.Left = Add(node.Left, item);
            }
            else
            {
                node.Right = Add(node.Right, item);
            }

            return node;
        }

        public int Count => _nodeCount;
        public bool IsReadOnly => false;

        private bool Contains(Node node, T elem)
        {
            if (node == null) return false;
            if (elem == null) return false;
            int result = _comparer.Compare(node.Data, elem);
            return result == 0 || Contains(result < 0 ? node.Right : node.Left, elem);
        }

        public IEnumerator<T> GetEnumerator()
        {
            switch (_traverse)
            {
                case Traverse.InOrder: return InOrderEnumerator();
                case Traverse.LevelOrder: return LevelOrderEnumerator();
                case Traverse.PostOrder: return PostOrderEnumerator();
                case Traverse.PreOrder: return PreOrderEnumerator();
            }
            throw new System.NotImplementedException();
        }

        private IEnumerator<T> PreOrderEnumerator()
        {
            if (_root == null) yield break;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_root);
            while (stack.Any())
            {
                Node current = stack.Pop();
                if (current.Right != null) stack.Push(current.Right); 
                if (current.Left != null) stack.Push(current.Left);
                yield return current.Data;
            }
        }
        
        private IEnumerator<T> PostOrderEnumerator()
        {
            if (_root == null) yield break;
            Stack<Node> stack1 = new Stack<Node>();
            Stack<Node> stack2 = new Stack<Node>();
            stack1.Push(_root);
            while (stack1.Any())
            {
                Node current = stack1.Pop();
                stack2.Push(current);
                if (current.Left != null) stack1.Push(current.Left);
                if (current.Right != null) stack1.Push(current.Right); 
            }

            while (stack2.Any())
                yield return stack2.Pop().Data;
        }

        private IEnumerator<T> LevelOrderEnumerator()
        {
            if (_root == null) yield break;
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(_root);

            while (queue.Any())
            {
                Node current = queue.Dequeue();
                yield return current.Data;
                if (current.Left != null)
                    queue.Enqueue(current.Left);
                if (current.Right != null)
                    queue.Enqueue(current.Right);
            }
        }

        private IEnumerator<T> InOrderEnumerator()
        {
            if (_root == null) yield break;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(_root);
            Node current = _root;

            while (stack.Any())
            {
                while (current?.Left != null)
                {
                    stack.Push(current.Left);
                    current = current.Left;
                }

                Node node = stack.Pop();
                if (node.Right != null)
                {
                    stack.Push(node.Right);
                    current = node.Right;
                }
                yield return node.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}