using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    public sealed class SequenceComponentFoldout : VisualElement
    {
        public static Color BackgroundColor
        {
            get
            {
                if (EditorGUIUtility.isProSkin) return new Color(0.15f, 0.15f, 0.15f);
                else return new Color(0.9f, 0.9f, 0.9f);
            }
        }

        public SequenceComponentFoldout()
        {
            foldout = new IMGUIContainer(() =>
            {
                var foldoutRect = EditorGUILayout.GetControlRect();
                var toggleRect = foldoutRect.AddXMin(16f).SetWidth(20f);
                var labelRect = foldoutRect.AddXMin(35f);

                IsActive = EditorGUI.ToggleLeft(toggleRect, string.Empty, IsActive);
                IsExpanded = EditorGUI.Foldout(foldoutRect, IsExpanded, string.Empty, true);

                EditorGUI.LabelField(labelRect, Label, EditorStyles.boldLabel);
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

        readonly IMGUIContainer foldout;
        readonly VisualElement contents;

        public override VisualElement contentContainer => contents;

        public event Action<bool> OnFoldoutStateChanged;
        public event Action<bool> OnCheckboxStateChanged;
    }
}