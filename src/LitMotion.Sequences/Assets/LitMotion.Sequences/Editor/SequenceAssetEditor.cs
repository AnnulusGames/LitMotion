using UnityEditor;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{

    [CustomEditor(typeof(SequenceAsset))]
    public sealed class SequenceAssetEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var element = new SequenceAssetView(serializedObject);
            return element;
        }
    }
}
