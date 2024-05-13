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

    [CustomEditor(typeof(MaterialPropertyComponentBase<>), editorForChildClasses: true)]
    internal sealed class MaterialComponentEditor : SequenceComponentBaseEditor
    {
        protected override GUIContent GetIconContent()
        {
            return EditorGUIUtility.IconContent("Material Icon");
        }
    }

    [CustomEditor(typeof(SpriteRendererColorComponent))]
    internal sealed class SpriteRendererColorComponentEditor : SequenceComponentBaseEditor
    {
        protected override GUIContent GetIconContent()
        {
            return EditorGUIUtility.IconContent("SpriteRenderer Icon");
        }
    }
}