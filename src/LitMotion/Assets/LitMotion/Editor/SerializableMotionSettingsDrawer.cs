using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace LitMotion.Editor
{
    [CustomPropertyDrawer(typeof(SerializableMotionSettings<,>))]
    public class SerializableMotionSettingsDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var foldout = new Foldout()
            {
                text = property.displayName,
                toggleOnLabelClick = true
            };

            foldout.BindProperty(property);

            Group(foldout, group =>
            {
                AddPropertyField(group, property, "startValue");
                AddPropertyField(group, property, "endValue");
                AddPropertyField(group, property, "duration");
            });

            Group(foldout, group =>
            {
                var ease = property.FindPropertyRelative("ease");
                group.Add(new PropertyField(ease));

                var customEaseCurve = new PropertyField(property.FindPropertyRelative("customEaseCurve"));
                customEaseCurve.style.display = ease.enumValueIndex == (int)Ease.CustomAnimationCurve ? DisplayStyle.Flex : DisplayStyle.None;

                group.Add(customEaseCurve);
                customEaseCurve.schedule
                    .Execute(() => customEaseCurve.style.display = ease.enumValueIndex == (int)Ease.CustomAnimationCurve ? DisplayStyle.Flex : DisplayStyle.None)
                    .Every(10);
            });

            Group(foldout, group =>
            {
                AddPropertyField(group, property, "delay");
                AddPropertyField(group, property, "delayType");
            });

            Group(foldout, group =>
            {
                AddPropertyField(group, property, "loops");
                AddPropertyField(group, property, "loopType");
            });

            var options = property.FindPropertyRelative("options");
            if (options.hasChildren)
            {
                Group(foldout, group =>
                {
                    var optionTypeName = ObjectNames.NicifyVariableName(fieldInfo.FieldType.GenericTypeArguments[1].Name);
                    FoldoutGroup(group, optionTypeName, options, group =>
                    {
                        group.style.marginLeft = 15f;
                        foreach (var child in GetChildren(options))
                        {
                            group.Add(new PropertyField(child.Copy()));
                        }
                    });
                });
            }

            Group(foldout, group =>
            {
                FoldoutGroup(group, "Additional Settings", property.FindPropertyRelative("additionalSettings"), group =>
                {
                    group.style.marginLeft = 15f;
                    AddPropertyField(group, property, "cancelOnError");
                    AddPropertyField(group, property, "skipValuesDuringDelay");
                    AddPropertyField(group, property, "bindOnSchedule");
                    group.Add(new PropertyField(property.FindPropertyRelative("schedulerType"), "Scheduler"));
                });
            });

            return foldout;
        }

        void Group(VisualElement root, Action<VisualElement> configure)
        {
            var group = new UnityEngine.UIElements.Box() { style = { marginBottom = 3f } };
            configure(group);
            root.Add(group);
        }

        void FoldoutGroup(VisualElement root, string label, SerializedProperty property, Action<VisualElement> configure)
        {
            var group = new Foldout
            {
                text = label,
                style = { marginBottom = 3f },
                toggleOnLabelClick = true
            };
            group.BindProperty(property);
            configure(group);
            root.Add(group);
        }

        void AddPropertyField(VisualElement root, SerializedProperty property, string name)
        {
            root.Add(new PropertyField(property.FindPropertyRelative(name)));
        }

        IEnumerable<SerializedProperty> GetChildren(SerializedProperty property)
        {
            var currentProperty = property.Copy();
            var nextProperty = property.Copy();
            nextProperty.Next(false);

            if (currentProperty.Next(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(currentProperty, nextProperty)) break;
                    yield return currentProperty;
                }
                while (currentProperty.Next(false));
            }
        }
    }
}
