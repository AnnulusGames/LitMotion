using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;

namespace LitMotion.Editor
{
    internal readonly struct SerializedFixedBytesView
    {
        static readonly Regex byteRegex = new(@"byte\d\d\d\d", RegexOptions.Compiled);
        readonly SerializedProperty property;
        readonly SerializedProperty lengthProperty;

        public SerializedFixedBytesView(SerializedProperty property)
        {
            this.property = property;
            this.lengthProperty = property.FindPropertyRelative("utf8LengthInBytes");
        }

        public SerializedProperty LengthProperty => lengthProperty;

        public byte[] GetBytes() => GetByteProperties()
            .Select(prop => (byte)prop.intValue)
            .Take(lengthProperty.intValue)
            .ToArray();

        public void SetBytes(ReadOnlySpan<byte> bytes)
        {
            var index = 0;
            foreach (var property in GetByteProperties())
            {
                if (index >= bytes.Length) break;

                property.intValue = bytes[index];
                index++;
            }

            lengthProperty.intValue = bytes.Length;
        }

        IEnumerable<SerializedProperty> GetByteProperties()
        {
            var property = this.property.Copy();
            var currentDepth = property.depth;

            while (property.Next(true) && property.depth > currentDepth)
            {
                if (byteRegex.IsMatch(property.name) && property.propertyType == SerializedPropertyType.Integer)
                {
                    yield return property.Copy();
                }
            }
        }
    }
}