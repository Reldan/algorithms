using System.Collections.Generic;

namespace Algorithms.DataStructures
{
    public class BinaryHeap<T> where T : notnull
    {
        private readonly T[] arr = new T[0];
        private readonly IComparer<T> _comparer;

        public BinaryHeap()
        {
            _comparer = Comparer<T>.Default;
        }

        public BinaryHeap(IComparer<T> comparer)
        {
            _comparer = comparer;
        }

        public void Add()
        {
            SortedList<string, string> s = new SortedList<string, string>();
        }
    }
}