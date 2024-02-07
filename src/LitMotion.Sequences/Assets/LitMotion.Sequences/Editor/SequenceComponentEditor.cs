using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
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
            iterator.NextVisible(true); // expanded

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

        protected virtual GUIContent GetIconContent() => EditorGUIUtility.IconContent("ScriptableObject Icon");

        protected virtual string GetCaption(SerializedObject serializedObject)
        {
            var caption = "";

            var delayProperty = serializedObject.FindProperty("delay");
            var hasDelay = delayProperty != null && delayProperty.floatValue > 0f;
            if (hasDelay)
            {
                caption += "Delay " + delayProperty.floatValue.ToString("F2") + "s";
            }

            var durationProperty = serializedObject.FindProperty("duration");
            if (durationProperty != null)
            {
                if (hasDelay) caption += " | ";
                caption += "Duration " + durationProperty.floatValue.ToString("F2") + "s";
            }

            var loopsProperty = serializedObject.FindProperty("loops");
            var hasLoop = loopsProperty != null && (loopsProperty.intValue is not (0 or 1));
            if (hasLoop)
            {
                caption += " | ";
                caption += "Loops x" + loopsProperty.intValue;
            }
            return caption;
        }

        protected SequenceComponentFoldout CreateFoldout()
        {
            var displayNameProperty = serializedObject.FindProperty("displayName");
            var enabledProperty = serializedObject.FindProperty("enabled");
            var expandedProperty = serializedObject.FindProperty("expanded");
            var foldout = new SequenceComponentFoldout
            {
                Label = displayNameProperty.stringValue,
                Caption = GetCaption(serializedObject),
                Icon = GetIconContent(),
                IsActive = enabledProperty.boolValue,
                IsExpanded = expandedProperty.boolValue,
            };
            foldout.TrackPropertyValue(displayNameProperty, property =>
            {
                foldout.Label = property.stringValue;
            });
            foldout.TrackSerializedObjectValue(serializedObject, so =>
            {
                foldout.Caption = GetCaption(so);
            });
            foldout.OnCheckboxStateChanged += x =>
            {
                enabledProperty.boolValue = x;
                serializedObject.ApplyModifiedProperties();
            };
            foldout.OnFoldoutStateChanged += x =>
            {
                expandedProperty.boolValue = x;
                serializedObject.ApplyModifiedProperties();
            };

            var displayNameField = new PropertyField(displayNameProperty);
            foldout.Add(displayNameField);

            return foldout;
        }
    }
}
