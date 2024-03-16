using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LitMotion.Collections
{
    /// <summary>
    /// A list of minimal features. Note that it is NOT thread-safe and must NOT be marked readonly as it is a mutable struct.
    /// </summary>
    /// <typeparam name="T">Element type</typeparam>
    [StructLayout(LayoutKind.Auto)]
    public struct FastListCore<T>
    {
        public FastListCore(int initialCapacity)
        {
            array = new T[initialCapacity];
            tailIndex = 0;
        }

        public static readonly FastListCore<T> Empty = new(0);

        T[] array;
        int tailIndex;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T element)
        {
            if (array.Length == tailIndex)
            {
                Array.Resize(ref array, tailIndex * 2);
            }

            array[tailIndex] = element;
            tailIndex++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAtSwapback(int index)
        {
            CheckIndex(index);
            array[index] = array[tailIndex - 1];
            array[tailIndex - 1] = default;
            tailIndex--;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            array.AsSpan().Clear();
            tailIndex = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int capacity)
        {
            while (array.Length < capacity)
            {
                Array.Resize(ref array, array.Length * 2);
            }
        }

        public readonly T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => array[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => array[index] = value;
        }

        public readonly int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => tailIndex;
        }

        public readonly Span<T> AsSpan() => array.AsSpan(0, tailIndex);
        public readonly T[] AsArray() => array;

        readonly void CheckIndex(int index)
        {
            if (index < 0 || index > tailIndex) throw new IndexOutOfRangeException();
        }
    }
}
