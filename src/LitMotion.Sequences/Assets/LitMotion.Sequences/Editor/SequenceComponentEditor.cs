using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceComponent), true, isFallback = true)]
    public class SequenceComponentEditor : UnityEditor.Editor
    {
        public sealed override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var foldout = CreateFoldout();
            root.Add(foldout);

            var iterator = serializedObject.GetIterator();
            iterator.NextVisible(true); // script field
            iterator.NextVisible(true); // displayName
            iterator.NextVisible(true); // enabled

            AddInspectorElements(foldout, iterator);
            return root;
        }

        protected virtual void AddInspectorElements(VisualElement root, SerializedProperty iterator)
        {
            while (iterator.NextVisible(false))
            {
                var field = new PropertyField(iterator);
                root.Add(field);
            }
        }

        protected SequenceComponentFoldout CreateFoldout()
        {
            var displayNameProperty = serializedObject.FindProperty("displayName");
            var enabledProperty = serializedObject.FindProperty("enabled");
            var foldout = new SequenceComponentFoldout
            {
                Label = displayNameProperty.stringValue,
                IsActive = enabledProperty.boolValue,
                IsExpanded = enabledProperty.isExpanded,
            };
            foldout.TrackPropertyValue(displayNameProperty, property =>
            {
                foldout.Label = property.stringValue;
            });
            foldout.OnCheckboxStateChanged += x =>
            {
                enabledProperty.boolValue = x;
                serializedObject.ApplyModifiedProperties();
            };
            foldout.OnFoldoutStateChanged += x =>
            {
                enabledProperty.isExpanded = x;
                serializedObject.ApplyModifiedProperties();
            };

            var displayNameField = new PropertyField(displayNameProperty);
            foldout.Add(displayNameField);

            return foldout;
        }
    }
}
