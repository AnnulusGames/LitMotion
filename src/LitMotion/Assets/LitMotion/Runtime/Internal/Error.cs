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

        public static void Format()
        {
            throw new FormatException("Input string was not in a correct format.");
        }

        public static void Argument(string message)
        {
            throw new ArgumentException(message);
        }

        public static void ArgumentNull(string message)
        {
            throw new ArgumentNullException(message);
        }

        public static void MotionNotExists()
        {
            throw new InvalidOperationException("Motion has been destroyed or no longer exists.");
        }

        public static void MotionHasBeenCanceledOrCompleted()
        {
            throw new InvalidOperationException("Motion has already been canceled or completed.");
        }

        public static void MotionIsInSequence()
        {
            throw new InvalidOperationException("Cannot access the motion in sequence.");
        }
    }
}