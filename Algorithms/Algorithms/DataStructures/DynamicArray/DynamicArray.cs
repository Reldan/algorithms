using System;

namespace Algorithms.DataStructures.DynamicArray
{
    public class DynamicArray<T>
    {
        private const int DefaultSize = 16;
        private const int ResizeFactor = 2;
        private static readonly int HalfMaxSize = Int32.MaxValue / 2;
        private T[] _arr;
        private int _capacity;
        private int _size = 0;


        public DynamicArray(int initialCapacity)
        {
            if (initialCapacity < 0) throw new ArgumentException("Oh should be not less than 0");
            _capacity = initialCapacity;
            _arr = new T[_capacity];
        }

        public void Add(T a)
        {
            if (_size == _capacity)
            {
                if (_capacity == int.MaxValue)
                {
                    throw new Exception("Oh it is too much");
                }
                Resize();
            }

            _arr[_size++] = a;
        }

        public T this[int i]
        {
            set
            {
                if (i < 0 || i >= _size) throw new ArgumentOutOfRangeException(nameof(i), "Should be from 0 till size - 1");
                _arr[i] = value;
            }
            get
            {
                if (i < 0 || i >= _size) throw new ArgumentOutOfRangeException(nameof(i), "Should be from 0 till size - 1");
                return _arr[i];
            }
        }

        public void RemoveAt(int i)
        {
            if (i < 0 || i >= _size) throw new ArgumentOutOfRangeException(nameof(i), "Should be from 0 till size - 1");
            if (i == _size - 1)
            {
                _arr[i] = default; // gc
            }
            else
            {
                for (int j = i; j < _size - 1; j++)
                {
                    _arr[j] = _arr[j + 1];
                }
            }
            _size -= 1;
        }

        public void Resize()
        {
            int newCapacity = _capacity < DefaultSize ? DefaultSize : NewSize;
            T[] newArr = new T[newCapacity];
            Array.Copy(_arr, newArr, _arr.Length);
            _arr = newArr;
            _capacity = newCapacity;
        }

        private int NewSize => _capacity > HalfMaxSize ? int.MaxValue : _capacity * ResizeFactor;
        
        
        
        public int Count => _size;
    }
}