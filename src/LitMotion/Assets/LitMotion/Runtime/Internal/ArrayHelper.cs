using System;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal static class ArrayHelper
    {
        const int ArrayMaxSize = 0x7FFFFFC7;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnsureCapacity<T>(ref T[] array, int minimumCapacity)
        {
            if (array == null)
            {
                array = new T[minimumCapacity];
            }
            else
            {
                var l = array.Length;
                if (l >= minimumCapacity) return;

                while (l < minimumCapacity)
                {
                    l *= 2;
                }

                Array.Resize(ref array, l);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void EnsureBufferCapacity(ref char[] buffer, int minimumCapacity)
        {
            if (buffer == null)
            {
                Error.ArgumentNull(nameof(buffer));
            }

            var current = buffer.Length;
            if (minimumCapacity > current)
            {
                int num = minimumCapacity;
                if (num < 256)
                {
                    num = 256;
                    FastResize(ref buffer, num);
                    return;
                }

                if (current == ArrayMaxSize)
                {
                    throw new InvalidOperationException("char[] size reached maximum size of array(0x7FFFFFC7).");
                }

                var newSize = unchecked(current * 2);
                if (newSize < 0)
                {
                    num = ArrayMaxSize;
                }
                else
                {
                    if (num < newSize)
                    {
                        num = newSize;
                    }
                }

                FastResize(ref buffer, num);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void FastResize(ref char[] array, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException(nameof(newSize));

            char[] array2 = array;
            if (array2 == null)
            {
                array = new char[newSize];
                return;
            }

            if (array2.Length != newSize)
            {
                char[] array3 = new char[newSize];
                Buffer.BlockCopy(array2, 0, array3, 0, (array2.Length > newSize) ? newSize : array2.Length);
                array = array3;
            }
        }
    }
}