namespace LitMotion
{
    /// <summary>
    /// Specifies the behavior when await is canceled.
    /// </summary>
    public enum CancelBahaviour
    {
        CancelAndCancelAwait,
        CompleteAndCancelAwait,
        CancelAwait,
        Cancel,
        Complete
    }
}