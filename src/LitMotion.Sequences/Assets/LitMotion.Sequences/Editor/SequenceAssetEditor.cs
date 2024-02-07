using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceAsset))]
    public sealed class SequenceAssetEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            root.Add(new Label("Settings")
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 12.5f,
                    marginTop = 5f, marginBottom = 2f
                },
            });

            var listView = new SequenceComponentListView(serializedObject);
            root.Add(listView);

            return root;
        }
    }
}
