namespace LitMotion.Sequences
{
    public interface IMotionSequenceItem
    {
        void Configure(MotionSequenceBufferWriter writer);
    }
}