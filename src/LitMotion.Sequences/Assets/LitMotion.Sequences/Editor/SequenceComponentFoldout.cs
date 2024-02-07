using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    public sealed class SequenceComponentFoldout : VisualElement
    {
        static GUIStyle BoldLabelStyle
        {
            get
            {
                if (boldLabelStyle == null)
                {
                    boldLabelStyle = new GUIStyle(EditorStyles.boldLabel)
                    {
                        fontSize = 11
                    };
                }
                return boldLabelStyle;
            }
        }
        static GUIStyle boldLabelStyle;

        public SequenceComponentFoldout()
        {
            foldout = new IMGUIContainer(() =>
            {
                var foldoutRect = EditorGUILayout.GetControlRect();
                var toggleRect = foldoutRect.AddXMin(16f).SetWidth(20f);
                var iconRect = foldoutRect.AddXMin(35f).SetWidth(18f).SetHeight(18f);
                var labelRect = foldoutRect.AddXMin(55f);

                IsActive = EditorGUI.ToggleLeft(toggleRect, string.Empty, IsActive);
                IsExpanded = EditorGUI.Foldout(foldoutRect, IsExpanded, string.Empty, true);

                EditorGUI.LabelField(iconRect, Icon);
                EditorGUI.LabelField(labelRect, Label, BoldLabelStyle);
            });
            hierarchy.Add(foldout);

            contents = new VisualElement
            {
                style = {
                    marginLeft = 10f
                }
            };
            hierarchy.Add(contents);
        }

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                contents.style.display = isExpanded ? DisplayStyle.Flex : DisplayStyle.None;

                if (isExpanded != value)
                {
                    isExpanded = value;
                    OnFoldoutStateChanged?.Invoke(value);
                }
            }
        }
        bool isExpanded;

        public bool IsActive
        {
            get => isActive;
            set
            {
                contents.SetEnabled(isActive);

                if (isActive != value)
                {
                    isActive = value;
                    OnCheckboxStateChanged?.Invoke(value);
                }
            }
        }
        bool isActive;

        public string Label { get; set; }
        public GUIContent Icon { get; set; } = EditorGUIUtility.IconContent("ScriptableObject Icon");

        readonly IMGUIContainer foldout;
        readonly VisualElement contents;

        public override VisualElement contentContainer => contents;

        public event Action<bool> OnFoldoutStateChanged;
        public event Action<bool> OnCheckboxStateChanged;
    }
}
