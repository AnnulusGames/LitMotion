using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace LitMotion.Sequences
{
    [AddComponentMenu("LitMotion/Sequence Player")]
    public sealed class SequencePlayer : MonoBehaviour, ISequencePropertyTable
    {
        [SerializeField] SequenceAsset asset;

        [SerializeField] List<PropertyName> propertyNameList;
        [SerializeField] List<UnityObject> objectList;

        MotionSequence sequence;

        public bool IsPlaying() => sequence != null && sequence.IsActive();

        public void Play()
        {
            if (IsPlaying()) throw new InvalidOperationException("Sequence is now playing.");
#if UNITY_EDITOR
            sequence = asset.CreateSequence(this);
            IsModified = true;
#else
            if (sequence == null) sequence = asset.CreateSequence(this); // cache sequence
#endif
            sequence.Play();
        }

        public void Complete()
        {
            sequence?.Complete();
        }

        public void Cancel()
        {
            sequence?.Cancel();
        }

#if UNITY_EDITOR
        internal bool IsModified { get; private set; }
        
        internal void CancelAndRestoreValues()
        {
            sequence?.Cancel();
            foreach (var component in asset.Components)
            {
                component.RestoreValues(this);
            }
            IsModified = false;
        }

        internal void PlayPreview()
        {
            if (asset == null) return;

            IsModified = true;
            sequence?.Complete();
            sequence = asset.CreateSequence(this);
            sequence.Play();
        }
#endif

        void IExposedPropertyTable.ClearReferenceValue(PropertyName id)
        {
            var index = propertyNameList.IndexOf(id);
            if (index != -1)
            {
                propertyNameList.RemoveAt(index);
                objectList.RemoveAt(index);
            }
        }

        UnityObject IExposedPropertyTable.GetReferenceValue(PropertyName id, out bool idValid)
        {
            var index = propertyNameList.IndexOf(id);
            idValid = index != -1;
            return idValid ? objectList[index] : null;
        }

        void IExposedPropertyTable.SetReferenceValue(PropertyName id, UnityObject value)
        {
            if (PropertyName.IsNullOrEmpty(id)) return;

            var index = propertyNameList.IndexOf(id);
            if (index != -1)
            {
                propertyNameList[index] = id;
                objectList[index] = value;
            }
            else if (value == null)
            {
                propertyNameList.Add(id);
                objectList.Add(value);
            }
        }

        static class InitialValueCache<TKey, TValue>
        {
            static InitialValueCache()
            {
                store = new();
                dictionaries.Add(store);
            }

            readonly static Dictionary<TKey, TValue> store;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static bool TryGet(TKey key, out TValue result)
            {
                return store.TryGetValue(key, out result);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public static void Set(TKey key, TValue value)
            {
                store[key] = value;
            }
        }

        static readonly MinimumList<IDictionary> dictionaries = new();

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            foreach (var dict in dictionaries.AsSpan()) dict.Clear();
        }

        void ISequencePropertyTable.SetInitialValue<TKey, TValue>(TKey key, TValue value)
        {
            InitialValueCache<(SequencePlayer, TKey), TValue>.Set((this, key), value);
        }

        bool ISequencePropertyTable.TryGetInitialValue<TKey, TValue>(TKey key, out TValue value)
        {
            return InitialValueCache<(SequencePlayer, TKey), TValue>.TryGet((this, key), out value);
        }
    }
}
