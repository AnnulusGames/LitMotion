using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequencePlayer))]
    public sealed class SequencePlayerEditor : UnityEditor.Editor
    {
        VisualElement bindingView;
        VisualElement overrideView;

        static readonly string ExposedNamePropertyName = "exposedName";

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            var player = (SequencePlayer)target;

            var assetProperty = serializedObject.FindProperty("asset");
            var assetField = new PropertyField(assetProperty);
            root.Add(assetField);

            assetField.RegisterValueChangeCallback(eventData =>
            {
                var property = eventData.changedProperty;
                if (property.objectReferenceValue == null)
                {
                    UpdateBindingView(null, null);
                    UpdateOverrideView(property);
                    return;
                }

                var player = (SequencePlayer)property.serializedObject.targetObject;
                var asset = (SequenceAsset)property.objectReferenceValue;

                SetExposedNames(player, asset);
                UpdateBindingView(player, asset);
                UpdateOverrideView(property);
            });

            var label = new Label("Bindings")
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 12f,
                    marginLeft = 4f, marginTop = 3f, marginBottom = 3f,
                }
            };
            root.Add(label);

            bindingView = new Box
            {
                style = {
                    paddingBottom = 1f, paddingTop = 1f, paddingLeft = 2f, paddingRight = 2f,
                    marginLeft = 4f,
                }
            };
            root.Add(bindingView);

            overrideView = new VisualElement();
            root.Add(overrideView);

            UpdateBindingView(player, player.asset);
            UpdateOverrideView(assetProperty);

            return root;
        }

        void UpdateBindingView(IExposedPropertyTable table, SequenceAsset asset)
        {
            bindingView.Clear();

            if (asset == null || asset.Components.Count == 0)
            {
                bindingView.Add(new Label("Empty")
                {
                    style = {
                        marginLeft = 4f,
                        paddingBottom = 3f, paddingTop = 3f, paddingLeft = 3f, paddingRight = 3f
                    }
                });
                return;
            }

            foreach (var component in asset.Components)
            {
                if (component == null) continue;

                var serializedObject = new SerializedObject(component, target);
                var iterator = serializedObject.GetIterator();
                while (iterator.NextVisible(true))
                {
                    if (iterator.propertyType == SerializedPropertyType.ExposedReference)
                    {
                        var property = iterator.Copy();
                        var label = component.displayName + " / " + property.displayName;

                        var field = new ObjectField(label)
                        {
                            objectType = property.GetPropertyType().GenericTypeArguments[0],
                            value = property.exposedReferenceValue
                        };
                        field.RegisterValueChangedCallback(x =>
                        {
                            var obj = x.newValue;
                            property.exposedReferenceValue = obj;
                            serializedObject.ApplyModifiedProperties();
                            SetExposedNames(table, asset);
                        });

                        bindingView.Add(field);
                        field.TrackPropertyValue(serializedObject.FindProperty("displayName"), property =>
                        {
                            var player = (SequencePlayer)target;
                            UpdateBindingView(player, player.asset);
                        });
                    }
                }
            }
        }

        void UpdateOverrideView(SerializedProperty assetProperty)
        {
            overrideView.Clear();

            overrideView.Add(new Label("Motions")
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 12.5f,
                    marginLeft = 4f, marginTop = 5f, marginBottom = 2f
                },
            });

            if (assetProperty.objectReferenceValue == null)
            {
                return;
            }

            var listView = new SequenceComponentListView(new SerializedObject(assetProperty.objectReferenceValue));
            listView.style.marginLeft = 2f;
            overrideView.Add(listView);
        }

        void SetExposedNames(IExposedPropertyTable table, SequenceAsset asset)
        {
            foreach (var component in asset.Components)
            {
                if (component == null) continue;

                var serializedObject = new SerializedObject(component);
                var iterator = serializedObject.GetIterator();
                while (iterator.NextVisible(true))
                {
                    if (iterator.name == ExposedNamePropertyName)
                    {
                        table.GetReferenceValue(iterator.stringValue, out var isValid);
                        if (!isValid)
                        {
                            table.SetReferenceValue(iterator.stringValue, null);
                        }
                    }
                }
            }
        }
    }
}
