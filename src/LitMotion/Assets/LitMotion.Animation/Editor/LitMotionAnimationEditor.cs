using UnityEngine.UIElements;
using UnityEditor;
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
                var field = new PropertyField(property.GetArrayElementAtIndex(i));
                root.Add(field);
            }

            var button = new Button()
            {
                text = "Add..."
            };

            root.Add(button);

            return root;
        }
    }
}