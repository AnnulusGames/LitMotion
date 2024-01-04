using System;
using System.Runtime.CompilerServices;

namespace LitMotion
{
    internal static class Error
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void IsNull<T>(T target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
        }
    }
}