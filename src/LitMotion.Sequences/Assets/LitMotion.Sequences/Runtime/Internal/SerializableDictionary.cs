using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LitMotion.Sequences
{
    [Serializable]
    internal sealed class SerializableDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, ISerializationCallbackReceiver
    {
        [Serializable]
        struct SerializableKeyValuePair
        {
            public TKey Key;
            public TValue Value;
        }

        [SerializeField] SerializableKeyValuePair[] array;
        [NonSerialized] readonly Dictionary<TKey, TValue> dictionary = new();

        public ICollection<TKey> Keys => dictionary.Keys;
        public ICollection<TValue> Values => dictionary.Values;

        public int Count => dictionary.Count;
        public bool IsReadOnly => false;

        public TValue this[TKey key]
        {
            get => dictionary[key];
            set => dictionary[key] = value;
        }

        public void Add(TKey key, TValue value) => dictionary.Add(key, value);
        public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);
        public bool Remove(TKey key) => dictionary.Remove(key);
        public bool TryGetValue(TKey key, out TValue value) => dictionary.TryGetValue(key, out value);
        public void Clear() => dictionary.Clear();

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => dictionary.GetEnumerator();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            Array.Resize(ref array, dictionary.Count);
            var index = 0;
            foreach (var kv in dictionary)
            {
                array[index] = new() { Key = kv.Key, Value = kv.Value };
                index++;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            dictionary.Clear();
            for (int i = 0; i < array.Length; i++)
            {
                var kv = array[i];
                dictionary.Add(kv.Key, kv.Value);
            }
        }
    }
}
