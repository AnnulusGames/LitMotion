using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace LitMotion.Animation.Editor
{
    [CustomEditor(typeof(LitMotionAnimation))]
    public sealed class LitMotionAnimationEditor : UnityEditor.Editor
    {
        VisualElement root;
        SerializedProperty componentsProperty;
        int prevArraySize;

        public override VisualElement CreateInspectorGUI()
        {
            root = new VisualElement();
            componentsProperty = serializedObject.FindProperty("components");
            prevArraySize = componentsProperty.arraySize;

            RefleshInspector(false);

            root.schedule.Execute(() =>
            {
                if (componentsProperty.arraySize != prevArraySize)
                {
                    RefleshInspector(true);
                }
                prevArraySize = componentsProperty.arraySize;
            })
            .Every(10);

            return root;
        }

        ContextualMenuManipulator CreateContextMenuManipulator(SerializedProperty property, int arrayIndex)
        {
            return new ContextualMenuManipulator(evt =>
            {
                evt.menu.AppendAction("Remove Component", x =>
                {
                    property.DeleteArrayElementAtIndex(arrayIndex);
                    RefleshInspector(true);
                });
            });
        }

        void RefleshInspector(bool applyModifiedProperties)
        {
            if (applyModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
            }

            root.Clear();

            for (int i = 0; i < componentsProperty.arraySize; i++)
            {
                var property = componentsProperty.GetArrayElementAtIndex(i);
                var view = CreateComponentGUI(property.Copy());
                CreateContextMenuManipulator(componentsProperty, i).target = view.Foldout.Q<Toggle>();
                view.EnabledToggle.BindProperty(property.FindPropertyRelative("enabled"));
                root.Add(view);
            }

            var button = new Button()
            {
                text = "Add..."
            };

            root.Add(button);
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
    }
}