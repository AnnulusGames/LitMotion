using System;

namespace LitMotion
{
    internal static class Error
    {
        public static void IsNull<T>(T target)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));
        }
    }
}