using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal static class Utf16StringHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt32(ref char[] buffer, ref int bufferOffset, int value)
        {
            WriteInt64(ref buffer, ref bufferOffset, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteInt64(ref char[] buffer, ref int bufferOffset, long value)
        {
            long num1 = value, num2, num3, num4, num5, div;

            if (value < 0)
            {
                if (value == long.MinValue) // -9223372036854775808
                {
                    ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 20);
                    buffer[bufferOffset++] = '-';
                    buffer[bufferOffset++] = '9';
                    buffer[bufferOffset++] = '2';
                    buffer[bufferOffset++] = '2';
                    buffer[bufferOffset++] = '3';
                    buffer[bufferOffset++] = '3';
                    buffer[bufferOffset++] = '7';
                    buffer[bufferOffset++] = '2';
                    buffer[bufferOffset++] = '0';
                    buffer[bufferOffset++] = '3';
                    buffer[bufferOffset++] = '6';
                    buffer[bufferOffset++] = '8';
                    buffer[bufferOffset++] = '5';
                    buffer[bufferOffset++] = '4';
                    buffer[bufferOffset++] = '7';
                    buffer[bufferOffset++] = '7';
                    buffer[bufferOffset++] = '5';
                    buffer[bufferOffset++] = '8';
                    buffer[bufferOffset++] = '0';
                    buffer[bufferOffset++] = '8';
                }

                ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 1);
                buffer[bufferOffset++] = '-';
                num1 = unchecked(-value);
            }

            // WriteUInt64(inlined)

            if (num1 < 10000)
            {
                if (num1 < 10) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 1); goto L1; }
                if (num1 < 100) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 2); goto L2; }
                if (num1 < 1000) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 3); goto L3; }
                ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 4); goto L4;
            }
            else
            {
                num2 = num1 / 10000;
                num1 -= num2 * 10000;
                if (num2 < 10000)
                {
                    if (num2 < 10) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 5); goto L5; }
                    if (num2 < 100) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 6); goto L6; }
                    if (num2 < 1000) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 7); goto L7; }
                    ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 8); goto L8;
                }
                else
                {
                    num3 = num2 / 10000;
                    num2 -= num3 * 10000;
                    if (num3 < 10000)
                    {
                        if (num3 < 10) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 9); goto L9; }
                        if (num3 < 100) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 10); goto L10; }
                        if (num3 < 1000) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 11); goto L11; }
                        ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 12); goto L12;
                    }
                    else
                    {
                        num4 = num3 / 10000;
                        num3 -= num4 * 10000;
                        if (num4 < 10000)
                        {
                            if (num4 < 10) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 13); goto L13; }
                            if (num4 < 100) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 14); goto L14; }
                            if (num4 < 1000) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 15); goto L15; }
                            ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 16); goto L16;
                        }
                        else
                        {
                            num5 = num4 / 10000;
                            num4 -= num5 * 10000;
                            if (num5 < 10000)
                            {
                                if (num5 < 10) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 17); goto L17; }
                                if (num5 < 100) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 18); goto L18; }
                                if (num5 < 1000) { ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 19); goto L19; }
                                ArrayHelper.EnsureBufferCapacity(ref buffer, bufferOffset + 20); goto L20;
                            }
                        L20:
                            buffer[bufferOffset++] = (char)('0' + (div = (num5 * 8389L) >> 23));
                            num5 -= div * 1000;
                        L19:
                            buffer[bufferOffset++] = (char)('0' + (div = (num5 * 5243L) >> 19));
                            num5 -= div * 100;
                        L18:
                            buffer[bufferOffset++] = (char)('0' + (div = (num5 * 6554L) >> 16));
                            num5 -= div * 10;
                        L17:
                            buffer[bufferOffset++] = (char)('0' + num5);
                        }
                    L16:
                        buffer[bufferOffset++] = (char)('0' + (div = (num4 * 8389L) >> 23));
                        num4 -= div * 1000;
                    L15:
                        buffer[bufferOffset++] = (char)('0' + (div = (num4 * 5243L) >> 19));
                        num4 -= div * 100;
                    L14:
                        buffer[bufferOffset++] = (char)('0' + (div = (num4 * 6554L) >> 16));
                        num4 -= div * 10;
                    L13:
                        buffer[bufferOffset++] = (char)('0' + num4);
                    }
                L12:
                    buffer[bufferOffset++] = (char)('0' + (div = (num3 * 8389L) >> 23));
                    num3 -= div * 1000;
                L11:
                    buffer[bufferOffset++] = (char)('0' + (div = (num3 * 5243L) >> 19));
                    num3 -= div * 100;
                L10:
                    buffer[bufferOffset++] = (char)('0' + (div = (num3 * 6554L) >> 16));
                    num3 -= div * 10;
                L9:
                    buffer[bufferOffset++] = (char)('0' + num3);
                }
            L8:
                buffer[bufferOffset++] = (char)('0' + (div = (num2 * 8389L) >> 23));
                num2 -= div * 1000;
            L7:
                buffer[bufferOffset++] = (char)('0' + (div = (num2 * 5243L) >> 19));
                num2 -= div * 100;
            L6:
                buffer[bufferOffset++] = (char)('0' + (div = (num2 * 6554L) >> 16));
                num2 -= div * 10;
            L5:
                buffer[bufferOffset++] = (char)('0' + num2);
            }
        L4:
            buffer[bufferOffset++] = (char)('0' + (div = (num1 * 8389L) >> 23));
            num1 -= div * 1000;
        L3:
            buffer[bufferOffset++] = (char)('0' + (div = (num1 * 5243L) >> 19));
            num1 -= div * 100;
        L2:
            buffer[bufferOffset++] = (char)('0' + (div = (num1 * 6554L) >> 16));
            num1 -= div * 10;
        L1:
            buffer[bufferOffset++] = (char)('0' + num1);
        }
    }
}