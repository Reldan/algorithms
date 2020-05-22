using System;

namespace Algorithms.DataStructures.FenwickTree
{
    public class FenwickTree
    {
        private readonly long[] tree;
        private readonly int N;
        
        // does not get used, O(n) construction.
        public FenwickTree(long[] values)
        {
            N = values.Length;

            tree = (long[]) values.Clone();

            for (int i = 0; i < N; i++)
            {
                int parent = i + Lsb(i + 1);
                if (parent < N)
                    tree[parent] += tree[i];
            }
        }

        public long PrefixSum(int i)
        {
            long sum = 0L;
            while (i >= 0)
            {
                sum += tree[i];
                i -= Lsb(i + 1); // i -= lsb(i)
            }
            return sum;
        }

        public long Sum(int left, int right)
        {
            if (right < left) throw new ArgumentException();
            return PrefixSum(right) - PrefixSum(left - 1);
        }

        public long Get(int i) => Sum(i, i);

        public void Add(int i, long value)
        {
            while (i < N)
            {
                tree[i] += value;
                i += Lsb(i + 1);
            }
        }
        
        public void Set(int i, long value)
        {
            Add(i, tree[i] + value);
        }
        
        public int Lsb(int a)
        {
            return a & -a;
        }

        public override string ToString()
        {
            return  $"Tree: [{string.Join(',', tree)}]";
        }
    }
}