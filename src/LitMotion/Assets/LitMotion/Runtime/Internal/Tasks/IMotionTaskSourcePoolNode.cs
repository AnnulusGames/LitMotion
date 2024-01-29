namespace LitMotion
{
    internal interface IMotionTaskSourcePoolNode<T> where T : class
    {
        ref T NextNode { get; }
    }
}