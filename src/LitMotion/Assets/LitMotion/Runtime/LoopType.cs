namespace LitMotion
{
    /// <summary>
    /// Specifies the behavior when repeating the animation with `WithLoops`.
    /// </summary>
    public enum LoopType : byte
    {
        /// <summary>
        /// Repeat from beginning.
        /// </summary>
        Restart = 0,
        /// <summary>
        /// Cycles back and forth between the end and start values.
        /// </summary>
        Flip = 1,
        /// <summary>
        /// Increase the value each time the repeats.
        /// </summary>
        Incremental = 2,
        /// <summary>
        /// After reaching the end value, it will play backwards to return to the start value.
        /// </summary>
        Yoyo = 3,
    }
}