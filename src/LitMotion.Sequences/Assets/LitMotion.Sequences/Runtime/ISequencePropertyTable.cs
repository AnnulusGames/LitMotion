using UnityEngine;

namespace LitMotion.Sequences
{
    public interface ISequencePropertyTable : IExposedPropertyTable
    {
        void SetInitialValue<TKey, TValue>(TKey key, TValue value);
        bool TryGetInitialValue<TKey, TValue>(TKey key, out TValue value);
    }
}