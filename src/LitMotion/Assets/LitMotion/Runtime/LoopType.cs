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
        Restart,
        /// <summary>
        /// Cycles back and forth between the end and start values.
        /// </summary>
        Yoyo,
        /// <summary>
        /// Increase the value each time the repeats.
        /// </summary>
        Incremental
    }
}