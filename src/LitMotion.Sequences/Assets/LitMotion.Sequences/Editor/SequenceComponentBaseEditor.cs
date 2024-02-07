using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceComponentBase<,>), true)]
    public class SequenceComponentBaseEditor : SequenceComponentEditor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var foldout = CreateFoldout();
            root.Add(foldout);

            var iterator = serializedObject.GetIterator();
            iterator.NextVisible(true); // script field
            iterator.NextVisible(true); // displayName
            iterator.NextVisible(true); // enabled
            iterator.NextVisible(true); // target

            while (iterator.NextVisible(false))
            {
                var field = new PropertyField(iterator);
                foldout.Add(field);
            }

            return root;
        }
    }
}
