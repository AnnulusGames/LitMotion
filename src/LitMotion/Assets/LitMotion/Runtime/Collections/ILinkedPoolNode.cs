namespace LitMotion.Collections
{
    /// <summary>
    /// An object pool node consisting of a linked list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILinkedPoolNode<T> where T : class
    {
        ref T NextNode { get; }
    }
}
