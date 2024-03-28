using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceComponentBase<,>), true)]
    public class SequenceComponentBaseEditor : SequenceComponentEditor
    {
        const string EaseAnimationCurvePropertyName = "easeAnimationCurve";

        protected override void AddInspectorElements(VisualElement root, SerializedProperty iterator)
        {
            iterator.NextVisible(true); // target

            while (iterator.NextVisible(false))
            {
                var field = new PropertyField(iterator);
                root.Add(field);

                if (iterator.name == EaseAnimationCurvePropertyName)
                {
                    var serializedObject = iterator.serializedObject;

                    field.schedule
                        .Execute(() => field.style.display = serializedObject.FindProperty("ease").enumValueIndex == (int)Ease.CustomAnimationCurve ? DisplayStyle.Flex : DisplayStyle.None)
                        .Every(10);
                }
            }
        }
    }
}
