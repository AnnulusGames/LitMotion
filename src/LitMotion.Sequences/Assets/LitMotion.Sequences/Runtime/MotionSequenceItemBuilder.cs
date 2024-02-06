using System.Runtime.CompilerServices;

namespace LitMotion.Sequences
{
    public readonly struct MotionSequenceItemBuilder
    {
        internal MotionSequenceItemBuilder(MinimumList<MotionHandle> buffer)
        {
            this.buffer = buffer;
        }

        internal readonly MinimumList<MotionHandle> buffer;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(MotionHandle handle) => buffer.Add(handle);
    }
}