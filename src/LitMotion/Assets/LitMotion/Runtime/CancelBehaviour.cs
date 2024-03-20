namespace LitMotion
{
    /// <summary>
    /// Specifies the behavior when await is canceled.
    /// </summary>
    public enum CancelBehaviour
    {
        CancelAndCancelAwait,
        CompleteAndCancelAwait,
        CancelAwait,
        Cancel,
        Complete
    }
}