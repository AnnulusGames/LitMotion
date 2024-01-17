namespace LitMotion
{
    /// <summary>
    /// Specifies the behavior of WithDelay.
    /// </summary>
    public enum DelayType : byte
    {
        /// <summary>
        /// Delay when starting playback
        /// </summary>
        FirstLoop = 0,
        /// <summary>
        /// Delay every loop
        /// </summary>
        EveryLoop = 1,
    }
}