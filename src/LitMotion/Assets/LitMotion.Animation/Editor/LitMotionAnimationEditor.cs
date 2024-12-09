using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using System.Collections;
using UnityEditor.UIElements;

namespace LitMotion.Animation.Editor
{
    [CustomEditor(typeof(LitMotionAnimation))]
    public sealed class LitMotionAnimationEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            var property = serializedObject.FindProperty("components");
            for (int i = 0; i < property.arraySize; i++)
            {
                var element = CreateComponentGUI(property.GetArrayElementAtIndex(i));
                root.Add(element);
            }

            var button = new Button()
            {
                text = "Add..."
            };

            root.Add(button);

            return root;
        }

        VisualElement CreateComponentGUI(SerializedProperty property)
        {
            var view = new AnimationComponentView()
            {
                Text = property.displayName
            };

            var endProperty = property.GetEndProperty();
            var isFirst = true;
            while (property.NextVisible(isFirst))
            {
                if (SerializedProperty.EqualContents(property, endProperty)) break;
                isFirst = false;

                view.Add(new PropertyField(property));
            }

            return view;
        }
    }
}