namespace LitMotion
{
    public static class LSequence
    {
        public static MotionSequenceBuilder Create()
        {
            var source = MotionSequenceBuilderSource.Rent();
            return new MotionSequenceBuilder(source);
        }
    }
}