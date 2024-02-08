using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using LitMotion.Sequences.Components;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceGroupComponent))]
    public sealed class SequenceGroupComponentEditor : SequenceComponentEditor
    {
        protected override void AddInspectorElements(VisualElement root, SerializedProperty iterator)
        {
            root.Add(new Label("Motions")
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 12.5f,
                    marginLeft = 4f, marginTop = 5f, marginBottom = 2f
                },
            });

            var listView = new SequenceComponentListView(serializedObject);
            listView.style.marginLeft = 2f;
            root.Add(listView);
        }

        protected override GUIContent GetIconContent()
        {
            return EditorGUIUtility.IconContent("VerticalLayoutGroup Icon");
        }
    }
}
