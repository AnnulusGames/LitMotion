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

        static GUIStyle CaptionStyle
        {
            get
            {
                if (captionStyle == null)
                {
                    captionStyle = new GUIStyle(EditorStyles.label)
                    {
                        alignment = TextAnchor.MiddleRight,
                        fontSize = 10,
                    };
                }
                return captionStyle;
            }
        }
        static GUIStyle captionStyle;

        static readonly GUIContent MenuIconContent = EditorGUIUtility.IconContent("_Menu@2x");

        public SequenceComponentFoldout()
        {
            foldout = new IMGUIContainer(() =>
            {
                var rect = EditorGUILayout.GetControlRect();
                var foldoutRect = rect.AddXMax(-20f);
                var toggleRect = rect.AddXMin(16f).SetWidth(20f);
                var iconRect = rect.AddXMin(35f).SetWidth(18f).SetHeight(18f);
                var labelRect = rect.AddXMin(55f);
                var captionRect = rect.AddXMax(-20f);
                var contextMenuButtonRect = rect.AddX(rect.width - 20f).SetWidth(20f).SetHeight(20f);
                var rightClickRect = foldoutRect.SetHeight(16f);

                var e = Event.current;

                IsActive = EditorGUI.ToggleLeft(toggleRect, string.Empty, IsActive);
                IsExpanded = EditorGUI.Foldout(foldoutRect, IsExpanded, string.Empty, true);

                EditorGUI.LabelField(iconRect, Icon);
                EditorGUI.LabelField(labelRect, Label, BoldLabelStyle);
                EditorGUI.LabelField(captionRect, Caption, CaptionStyle);
                EditorGUI.LabelField(contextMenuButtonRect, MenuIconContent);

                if (e.type == EventType.MouseDown)
                {
                    if (e.button == 0 && contextMenuButtonRect.Contains(e.mousePosition))
                    {
                        e.Use();
                        OnContextButtonClicked?.Invoke();
                    }
                    else if (e.button == 1 && rightClickRect.Contains(e.mousePosition))
                    {
                        e.Use();
                        OnContextButtonClicked?.Invoke();
                    }
                }
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
        public string Caption { get; set; }
        public GUIContent Icon { get; set; }

        readonly IMGUIContainer foldout;
        readonly VisualElement contents;

        public override VisualElement contentContainer => contents;

        public event Action<bool> OnFoldoutStateChanged;
        public event Action<bool> OnCheckboxStateChanged;
        public Action OnContextButtonClicked;

        public void ResetContextButtonEvents()
        {
            OnContextButtonClicked = null;
        }
    }
}
