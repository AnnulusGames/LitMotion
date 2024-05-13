using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    public sealed class SequenceAssetView : VisualElement
    {
        public SequenceAssetView(SerializedObject serializedObject)
        {
            var header = new VisualElement
            {
                style = {
                    flexDirection = FlexDirection.Row,
                }
            };
            Add(header);

            header.Add(new Label("Motions")
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    flexGrow = 1f,
                    fontSize = 12.5f,
                    marginTop = 5f, marginBottom = 2.5f
                },
            });

            var field = new EnumField(SequenceAsset.PlayMode.Sequential)
            {
                style = {
                    alignSelf = Align.FlexEnd,
                    width = 95f,
                },
                bindingPath = "playMode"
            };
            header.Add(field);

            var listView = new SequenceComponentListView(serializedObject);
            Add(listView);

            this.Bind(serializedObject);
        }
    }
}
