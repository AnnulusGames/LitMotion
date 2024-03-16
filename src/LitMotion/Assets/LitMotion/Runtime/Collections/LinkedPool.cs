using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace LitMotion.Collections
{
    /// <summary>
    /// Thread-safe linked list object pool
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [StructLayout(LayoutKind.Auto)]
    public struct LinkedPool<T> where T : class, ILinkedPoolNode<T>
    {
        static readonly int MaxPoolSize = int.MaxValue;

        int gate;
        int size;
        T root;

        public readonly int Size => size;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPop(out T result)
        {
            if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
            {
                var v = root;
                if (v != null)
                {
                    ref var nextNode = ref v.NextNode;
                    root = nextNode;
                    nextNode = null;
                    size--;
                    result = v;
                    Volatile.Write(ref gate, 0);
                    return true;
                }

                Volatile.Write(ref gate, 0);
            }
            result = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryPush(T item)
        {
            if (Interlocked.CompareExchange(ref gate, 1, 0) == 0)
            {
                if (size < MaxPoolSize)
                {
                    item.NextNode = root;
                    root = item;
                    size++;
                    Volatile.Write(ref gate, 0);
                    return true;
                }
                else
                {
                    Volatile.Write(ref gate, 0);
                }
            }
            return false;
        }
    }
}