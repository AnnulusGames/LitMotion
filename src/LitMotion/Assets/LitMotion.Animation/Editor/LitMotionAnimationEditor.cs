using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace LitMotion.Animation.Editor
{
    [CustomEditor(typeof(LitMotionAnimation))]
    public sealed class LitMotionAnimationEditor : UnityEditor.Editor
    {
        VisualElement root;
        SerializedProperty componentsProperty;
        int prevArraySize;

        AddAnimationComponentDropdown dropdown;
        Button addButton;
        List<AnimationComponentView> views = new();

        public override VisualElement CreateInspectorGUI()
        {
            root = new VisualElement();
            componentsProperty = serializedObject.FindProperty("components");
            prevArraySize = componentsProperty.arraySize;

            dropdown = new AddAnimationComponentDropdown(new());
            dropdown.OnTypeSelected += type =>
            {
                var last = componentsProperty.arraySize;
                componentsProperty.InsertArrayElementAtIndex(componentsProperty.arraySize);
                var property = componentsProperty.GetArrayElementAtIndex(last);
                property.managedReferenceValue = ReflectionHelper.CreateDefaultInstance(type);
                serializedObject.ApplyModifiedProperties();
            };

            RefleshInspector(false);

            root.schedule.Execute(() =>
            {
                if (componentsProperty.arraySize != prevArraySize)
                {
                    RefleshInspector(true);
                    prevArraySize = componentsProperty.arraySize;
                }

                var components = ((LitMotionAnimation)target).Components;
                for (int i = 0; i < components.Count; i++)
                {
                    var handle = components[i].TrackedHandle;
                    views[i].Progress = handle.IsActive()
                        ? Mathf.InverseLerp(0f, (float)handle.Duration, (float)handle.Time)
                        : 0;
                }
            })
            .Every(10);

            return root;
        }

        void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        void OnDisable()
        {
            if (!EditorApplication.isPlayingOrWillChangePlaymode && target != null)
            {
                ((LitMotionAnimation)target).Handle.TryCancel();
            }

            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                ((LitMotionAnimation)target).Handle.TryCancel();
            }
        }

        void RefleshInspector(bool applyModifiedProperties)
        {
            if (applyModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
            }

            root.Clear();
            views.Clear();

            root.schedule.Execute(() =>
            {
                var enabled = !((LitMotionAnimation)target).Handle.IsActive();
                foreach (var view in views)
                {
                    view.SetEnabled(enabled);
                }
            }).Every(10);

            for (int i = 0; i < componentsProperty.arraySize; i++)
            {
                var property = componentsProperty.GetArrayElementAtIndex(i);
                var view = CreateComponentGUI(property.Copy());
                CreateContextMenuManipulator(componentsProperty, i, false).target = view.Foldout.Q<Toggle>();
                view.EnabledToggle.BindProperty(property.FindPropertyRelative("enabled"));
                root.Add(view);
                views.Add(view);
                CreateContextMenuManipulator(componentsProperty, i, true).target = view.ContextMenuButton;
            }

            addButton = new Button(() => dropdown.Show(addButton.worldBound))
            {
                text = "Add...",
                style = {
                    width = 200f,
                    alignSelf = Align.Center
                }
            };
            root.Add(addButton);

            var box = new Box
            {
                style = {
                    marginTop = 6f,
                    marginBottom = 2f,
                    alignItems = Align.FlexStart,
                    flexDirection = FlexDirection.Column
                }
            };
            box.Add(new Label("Debug")
            {
                style = {
                    marginTop = 5f,
                    marginBottom = 2f,
                    marginLeft = 5f,
                    unityFontStyleAndWeight = FontStyle.Bold
                }
            });
            var controlPanel = new VisualElement
            {
                style = {
                    flexDirection = FlexDirection.Row,
                    flexGrow = 1f,
                }
            };
            controlPanel.Add(new Button(() => ((LitMotionAnimation)target).Play())
            {
                text = "Play",
                style = {
                    flexGrow = 1f,
                }
            });
            controlPanel.Add(new Button(() => ((LitMotionAnimation)target).Handle.TryCancel())
            {
                text = "Stop",
                style = {
                    flexGrow = 1f,
                }
            });
            box.Add(controlPanel);
            root.Add(box);
        }

        AnimationComponentView CreateComponentGUI(SerializedProperty property)
        {
            var view = new AnimationComponentView
            {
                Text = property.FindPropertyRelative("displayName").stringValue
            };

            var targetProperty = property.FindPropertyRelative("target");
            if (targetProperty != null)
            {
                view.Icon = AssetPreview.GetMiniTypeThumbnail(targetProperty.GetPropertyType());
            }

            view.TrackPropertyValue(property.FindPropertyRelative("displayName"), x =>
            {
                view.Text = x.stringValue;
            });

            view.Foldout.BindProperty(property);

            var endProperty = property.GetEndProperty();
            var isFirst = true;
            while (property.NextVisible(isFirst))
            {
                if (SerializedProperty.EqualContents(property, endProperty)) break;
                if (property.name == "enabled") continue;
                isFirst = false;

                view.Add(new PropertyField(property));
            }

            return view;
        }

        ContextualMenuManipulator CreateContextMenuManipulator(SerializedProperty property, int arrayIndex, bool activeLeftClick)
        {
            var manipulator = new ContextualMenuManipulator(evt =>
            {
                evt.menu.AppendAction("Remove Component", x =>
                {
                    property.DeleteArrayElementAtIndex(arrayIndex);
                    RefleshInspector(true);
                });
            });

            if (activeLeftClick)
            {
                manipulator.activators.Add(new ManipulatorActivationFilter
                {
                    button = MouseButton.LeftMouse,
                });
            }

            return manipulator;
        }
    }
}