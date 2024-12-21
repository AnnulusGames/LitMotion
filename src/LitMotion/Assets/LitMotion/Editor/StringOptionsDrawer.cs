using Unity.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Editor
{
    [CustomPropertyDrawer(typeof(StringOptions))]
    public sealed class StringOptionsDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var foldout = new Foldout
            {
                text = property.displayName,
            };
            foldout.BindProperty(property);

            var scrambleModeProperty = property.FindPropertyRelative("ScrambleMode");
            foldout.Add(new PropertyField(scrambleModeProperty));

            var customScrambleCharsField = PropertyFieldHelper.CreateFixedStringField(property.FindPropertyRelative("CustomScrambleChars"), FixedString64Bytes.UTF8MaxLengthInBytes);
            customScrambleCharsField.schedule.Execute(() =>
            {
                customScrambleCharsField.style.display = scrambleModeProperty.enumValueIndex == (int)ScrambleMode.Custom
                    ? DisplayStyle.Flex
                    : DisplayStyle.None;
            }).Every(10);
            foldout.Add(customScrambleCharsField);

            foldout.Add(new PropertyField(property.FindPropertyRelative("RichTextEnabled")));
            foldout.Add(new PropertyField(property.FindPropertyRelative("RandomSeed")));

            return foldout;
        }
    }
}