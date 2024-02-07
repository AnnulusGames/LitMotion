using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceComponentBase<,>), true)]
    public class SequenceComponentBaseEditor : SequenceComponentEditor
    {
        protected override void AddInspectorElements(VisualElement root, SerializedProperty iterator)
        {
            iterator.NextVisible(true); // target

            while (iterator.NextVisible(false))
            {
                var field = new PropertyField(iterator);
                root.Add(field);
            }
        }
    }
}
