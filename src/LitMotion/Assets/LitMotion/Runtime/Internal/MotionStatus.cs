namespace LitMotion
{
    /// <summary>
    /// Motion status.
    /// </summary>
    internal enum MotionStatus : byte
    {
        None = 0,
        Scheduled = 1,
        Delayed = 2,
        Playing = 3,
        Completed = 4,
        Canceled = 5,
        Disposed = 6,
    }
}