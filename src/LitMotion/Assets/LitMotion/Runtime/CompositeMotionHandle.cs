using System;
using System.Collections;
using System.Collections.Generic;

namespace LitMotion
{
    /// <summary>
    /// A class that manages multiple motion handles at once.
    /// </summary>
    public sealed class CompositeMotionHandle : ICollection<MotionHandle>, IEnumerable<MotionHandle>
    {
        public CompositeMotionHandle()
        {
            handleList = new();
        }

        public CompositeMotionHandle(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("capacity");
            }
            
            handleList = new(capacity);
        }

        /// <summary>
        /// Cancel all Motion handles and clear list.
        /// </summary>
        public void Cancel()
        {
            for (int i = 0; i < handleList.Count; i++)
            {
                var handle = handleList[i];
                if (handle.IsActive()) handle.Cancel();
            }
            handleList.Clear();
        }

        /// <summary>
        /// Complete all motion handles and clear list.
        /// </summary>
        public void Complete()
        {
            for (int i = 0; i < handleList.Count; i++)
            {
                var handle = handleList[i];
                if (handle.IsActive()) handle.Complete();
            }
            handleList.Clear();
        }

        /// <summary>
        /// Add motion handle.
        /// </summary>
        /// <param name="handle">Motion handle</param>
        public void Add(MotionHandle handle)
        {
            handleList.Add(handle);
        }

        public List<MotionHandle>.Enumerator GetEnumerator()
        {
            return handleList.GetEnumerator();
        }

        IEnumerator<MotionHandle> IEnumerable<MotionHandle>.GetEnumerator()
        {
            return handleList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return handleList.GetEnumerator();
        }

        public void Clear()
        {
            handleList.Clear();
        }

        public bool Contains(MotionHandle item)
        {
            return handleList.Contains(item);
        }

        public void CopyTo(MotionHandle[] array, int arrayIndex)
        {
            handleList.CopyTo(array, arrayIndex);
        }

        public bool Remove(MotionHandle item)
        {
            return handleList.Remove(item);
        }

        public int Count => handleList.Count;
        public bool IsReadOnly => false;

        readonly List<MotionHandle> handleList;
    }
}