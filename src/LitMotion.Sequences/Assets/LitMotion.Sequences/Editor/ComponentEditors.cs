using UnityEditor;
using UnityEngine;
using LitMotion.Sequences.Components;

namespace LitMotion.Sequences.Editor
{
    internal abstract class TransformComponentEditorBase : SequenceComponentBaseEditor
    {
        protected override GUIContent GetIconContent()
        {
            return EditorGUIUtility.IconContent("Transform Icon");
        }
    }

    [CustomEditor(typeof(TransformPositionComponent))]
    internal sealed class TransformPositionComponentEditor : TransformComponentEditorBase { }

    [CustomEditor(typeof(TransformRotationComponent))]
    internal sealed class TransformRotationComponentEditor : TransformComponentEditorBase { }

    [CustomEditor(typeof(TransformScaleComponent))]
    internal sealed class TransformScaleComponentEditor : TransformComponentEditorBase { }
}