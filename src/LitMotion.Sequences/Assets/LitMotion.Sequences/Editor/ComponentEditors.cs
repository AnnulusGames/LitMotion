using LitMotion.Sequences.Components;
using UnityEditor;
using UnityEngine;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(TransformPositionSequenceComponent))]
    internal sealed class TransformSequenceComponentEditor : SequenceComponentBaseEditor
    {
        protected override GUIContent GetIconContent()
        {
            return EditorGUIUtility.IconContent("Transform Icon");
        }
    }
}