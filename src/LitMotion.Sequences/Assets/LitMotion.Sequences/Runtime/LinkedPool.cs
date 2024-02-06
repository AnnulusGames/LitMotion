namespace LitMotion.Sequences
{
    internal interface ILinkedPoolNode<T> where T : class
    {
        ref T NextNode { get; }
    }

    // mutable struct, don't mark readonly
    internal struct LinkedPool<T> where T : class, ILinkedPoolNode<T>
    {
        T RootNode;

        public bool TryGet(out T result)
        {
            if (RootNode == null)
            {
                result = default;
                return false;
            }

            result = RootNode;
            RootNode = result.NextNode;
            result.NextNode = default;
            return true;
        }

        public void Return(T item)
        {
            item.NextNode = RootNode;
            RootNode = item;
        }
    }
}