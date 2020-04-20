using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Algorithms.DataStructures.BlockingQueue
{
    public class BlockingQueue<T>
    {
        private Queue<T> _queue;
        private int _size;

        public BlockingQueue(int size)
        {
            _size = size;
        }

        public void Enqueue(T a)
        {
            lock (_queue)
            {
                while (_queue.Count >= _size)
                {
                    Monitor.Wait(_queue);
                }
                _queue.Enqueue(a);
                if (_queue.Count == 1)
                {
                    Monitor.PulseAll(_queue);
                }
            }
        }

        public T Dequeue()
        {
            lock (_queue)
            {
                while (_queue.Count == 0)
                {
                    Monitor.Wait(_queue);
                }

                var result = _queue.Dequeue();
                if (_queue.Count == _size - 1)
                {
                    Monitor.PulseAll(_queue);
                }

                return result;
            }
        }
    }
}