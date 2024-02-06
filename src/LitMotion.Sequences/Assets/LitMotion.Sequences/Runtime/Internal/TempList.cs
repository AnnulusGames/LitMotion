using System;
using System.Buffers;
using System.Runtime.CompilerServices;

namespace LitMotion.Sequences
{
    internal struct TempList<T>
    {
        int index;
        T[] array;

        public TempList(int initialCapacity)
        {
            this.array = ArrayPool<T>.Shared.Rent(initialCapacity);
            this.index = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T value)
        {
            if (array.Length <= index)
            {
                var newArray = ArrayPool<T>.Shared.Rent(index * 2);
                Array.Copy(array, newArray, index);
                ArrayPool<T>.Shared.Return(array, true);
                array = newArray;
            }

            array[index++] = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ReadOnlySpan<T> AsSpan()
        {
            return new ReadOnlySpan<T>(array, 0, index);
        }

        public void Dispose()
        {
            ArrayPool<T>.Shared.Return(array, true);
        }
    }
}