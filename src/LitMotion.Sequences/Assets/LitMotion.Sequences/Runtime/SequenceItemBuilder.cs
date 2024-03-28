using System.Runtime.CompilerServices;

namespace LitMotion.Sequences
{
    public readonly struct SequenceItemBuilder
    {
        internal SequenceItemBuilder(FastList<MotionHandle> buffer)
        {
            this.buffer = buffer;
        }

        internal readonly FastList<MotionHandle> buffer;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(MotionHandle handle) => buffer.Add(handle);
    }
}