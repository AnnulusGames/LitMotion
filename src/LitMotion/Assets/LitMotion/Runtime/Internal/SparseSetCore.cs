using System;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal sealed class SparseSetCore
    {
        public struct Slot : IEquatable<Slot>
        {
            public int Next;
            public int DenseIndex;
            public int Version;

            public readonly bool Equals(Slot other)
            {
                return other.Next == Next && other.DenseIndex == DenseIndex && other.Version == Version;
            }

            public override readonly bool Equals(object obj)
            {
                return obj is Slot entry && Equals(entry);
            }

            public override readonly int GetHashCode()
            {
                return HashCode.Combine(Next, DenseIndex, Version);
            }
        }

        Slot[] slots;
        int freeSlot = -1;

        public int Capacity => slots.Length;

        public SparseSetCore(int initialCapacity = 32)
        {
            EnsureCapacity(initialCapacity);
        }

        public void EnsureCapacity(int capacity)
        {
            int prevLength;

            if (slots == null)
            {
                slots = new Slot[capacity];
                prevLength = 0;
            }
            else
            {
                prevLength = slots.Length;
                if (prevLength >= capacity) return;

                var newLength = prevLength;
                while (newLength < capacity)
                {
                    newLength *= 2;
                }

                Array.Resize(ref slots, newLength);
            }

            var span = slots.AsSpan(prevLength);

            for (int i = 0; i < span.Length; i++)
            {
                var index = prevLength + i;
                span[i] = new()
                {
                    Next = index == capacity - 1 ? freeSlot : index + 1,
                    DenseIndex = -1,
                    Version = 1
                };
            }

            freeSlot = prevLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SparseIndex Alloc(int denseIndex)
        {
            if (freeSlot == -1)
            {
                EnsureCapacity(slots.Length * 2);
            }

            var slotIndex = freeSlot;

            ref var slot = ref slots[slotIndex];
            freeSlot = slot.Next;
            slot.Next = -1;
            slot.DenseIndex = denseIndex;
            if (slot.Version == 0)
            {
                slot.Version = 1;
            }

            return new SparseIndex(slotIndex, slot.Version);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Free(SparseIndex sparseIndex)
        {
            ref var slot = ref slots[sparseIndex.Index];

            slot.Next = freeSlot;
            slot.Version++;
            freeSlot = sparseIndex.Index;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ref Slot GetSlotRefUnchecked(int index)
        {
            return ref slots[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i] = new()
                {
                    Next = i == slots.Length - 1 ? -1 : i + 1,
                    DenseIndex = -1,
                    Version = 1
                };
            }

            freeSlot = 0;
        }
    }
}