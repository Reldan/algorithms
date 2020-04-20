using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms
{
    public class Operation<TIn, TOut>
    {
        public Func<TIn, TOut> Mapper;
        public Func<TOut, TOut, TOut> Merger;
        public bool IsOverlapFriendly;

        public Operation(
            Func<TIn, TOut> mapper,
            Func<TOut, TOut, TOut> merger,
            bool isOverlapFriendly)
        {
            Merger = merger;
            Mapper = mapper;
            IsOverlapFriendly = isOverlapFriendly;
        }
    }
    
    public class SparseTable<TIn, TOut>
    {
        private TIn[] _original;
        private Operation<TIn, TOut> _operation;
        private int[] _log2;
        private TOut[][] _dp;
        private int[][] _it;
        private int _p; //maximum power of 2 needed. floor(log2(n))
        private int _n;
        
        public SparseTable(IEnumerable<TIn> source, Operation<TIn, TOut> operation)
        {
            _original = source.ToArray();
            _operation = operation;
            _n = _original.Length;
            
            _p = (int) Math.Floor(Math.Log2(_n));
            _dp = new TOut[_p + 1][];
            if (operation.IsOverlapFriendly)
                _it = new int[_p + 1][];
            
            for (int i = 0; i <= _p; i++) {
                _dp[i] = new TOut[_n];
                if (operation.IsOverlapFriendly)
                    _it[i] = new int[_n];
            }

            for (int i = 0; i < _n; i++) {
                _dp[0][i] = _operation.Mapper(_original[i]);
                if (operation.IsOverlapFriendly)
                    _it[0][i] = i;
            }

            _log2 = new int[_n + 1]; // 0 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16
                                     // 0 1 2 2 3 3 3 3 4 4 4  4  4  4  4  4  5
            for (int i = 2; i <= _n; i++) {
                _log2[i] = _log2[i / 2] + 1;
            }

            for (int i = 0; i < _p; i++) {
                for (int j = 0; j + (1 << (i + 1)) <= _n; j++)
                {
                    TOut leftInterval = _dp[i][j];
                    TOut rightInterval = _dp[i][j + (1 << i)];
                    _dp[i + 1][j] = operation.Merger(leftInterval, rightInterval);
                    if (operation.IsOverlapFriendly)
                    {
                        int index = operation.Merger(leftInterval, rightInterval).Equals(leftInterval)
                            ? j
                            : j + (1 << i);
                        _it[i + 1][j] = _it[i][index];
                    }
                }
            }
        }

        public TOut Query(int l, int r)
        {
            if (l < 0)
                throw new ArgumentException("Left boundary cannot be less than 0", nameof(l));
            if (r < l)
                throw new ArgumentException("Right boundary cannot be less than left boundary", nameof(r));
            if (r >= _n)
                throw new ArgumentException("Right boundary cannot be bigger than amount of elements", nameof(r));
            int lIndex = l;
            int rIndex = Math.Min(r, _n - 1);
            var length = rIndex - lIndex + 1;
            int layer = _log2[length];

            if (_operation.IsOverlapFriendly)
            {
                return _operation.Merger(_dp[layer][lIndex], _dp[layer][rIndex - length + 1]);
            }

            TOut acc = _dp[layer][lIndex];
            lIndex += 1 << layer;
            for (int p = _log2[rIndex - lIndex + 1]; lIndex <= rIndex; p = _log2[rIndex - lIndex + 1])
            {
                acc = _operation.Merger(acc, _dp[p][lIndex]);
                lIndex += (1 << p);
            }

            return acc;
        }

        public override string ToString()
        {
            var originalStr = string.Join("\t", _original);
            var table = string.Join('\n', _dp.Select(line => string.Join("\t", line)));
            return $"Original\n{originalStr}\nTable:\n{table}";
        }
    }
}