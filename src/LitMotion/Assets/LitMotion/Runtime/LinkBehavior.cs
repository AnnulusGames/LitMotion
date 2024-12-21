namespace LitMotion
{
    /// <summary>
    /// Specifies the behavior when linking motion to GameObject with AddTo.
    /// </summary>
    public enum LinkBehavior
    {
        CancelOnDestroy = 0,
        CancelOnDisable = 1,
        CompleteOnDisable = 2
    }
}
