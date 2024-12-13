using System.Buffers;
using System.Text;
using UnityEditor;
using UnityEngine.UIElements;

namespace LitMotion.Editor
{
    internal static class PropertyFieldHelper
    {
        public static VisualElement CreateFixedStringField(SerializedProperty property, int maxLength)
        {
            var textField = new TextField(property.displayName)
            {
                maxLength = maxLength
            };

            // set initial value
            var byteProperties = new SerializedFixedBytesView(property);
            textField.SetValueWithoutNotify(Encoding.UTF8.GetString(byteProperties.GetBytes()));

            textField.RegisterValueChangedCallback(x =>
            {
                var buffer = ArrayPool<byte>.Shared.Rent(x.newValue.Length * 3);
                try
                {
                    var lengthProperty = property.FindPropertyRelative("utf8LengthInBytes");
                    Encoding.UTF8.GetBytes(x.newValue, buffer);

                    byteProperties.SetBytes(buffer);
                    lengthProperty.intValue = buffer.Length;

                    property.serializedObject.ApplyModifiedProperties();
                }
                finally
                {
                    ArrayPool<byte>.Shared.Return(buffer);
                }
            });

            textField.AddToClassList("unity-base-field__aligned");

            return textField;
        }
    }
}