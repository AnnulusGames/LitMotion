using System;
using System.Runtime.CompilerServices;
using LitMotion.Collections;

namespace LitMotion.Sequences
{
    internal sealed class FastList<T>
    {
        FastListCore<T> core;

        public T this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => core[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => core[index] = value;
        }

        public int Length
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => core.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(T element) => core.Add(element);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RemoveAtSwapback(int index) => core.RemoveAtSwapback(index);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear() => core.Clear();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Span<T> AsSpan() => core.AsSpan();
    }
}