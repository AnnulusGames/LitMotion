using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace LitMotion
{
    // This implementation is based on UniTask's TaskPool<T>
    // Reference: https://github.com/Cysharp/UniTask/blob/64792b672d35e43b3412fc74861f8bdbf41e3a6f/src/UniTask/Assets/Plugins/UniTask/Runtime/TaskPool.cs

    [StructLayout(LayoutKind.Auto)]
    internal struct MotionTaskSourcePool<T> where T : class, IMotionTaskSourcePoolNode<T>
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