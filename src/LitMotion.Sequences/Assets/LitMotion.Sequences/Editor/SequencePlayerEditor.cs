using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequencePlayer))]
    public sealed class SequencePlayerEditor : UnityEditor.Editor
    {
        VisualElement root;
        Box bindingView;

        static readonly string ExposedNamePropertyName = "exposedName";

        public override VisualElement CreateInspectorGUI()
        {
            root = new();
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
                    return;
                }

                var player = (SequencePlayer)property.serializedObject.targetObject;
                var asset = (SequenceAsset)property.objectReferenceValue;

                SetExposedNames(player, asset);
                UpdateBindingView(player, asset);
            });

            var foldout = new Foldout
            {
                text = "Bindings"
            };
            root.Add(foldout);

            bindingView = new Box
            {
                style = {
                    paddingBottom = 1f, paddingTop = 1f, paddingLeft = 2f, paddingRight = 2f
                }
            };
            foldout.Add(bindingView);

            UpdateBindingView(player, player.asset);

            return root;
        }

        void UpdateBindingView(IExposedPropertyTable table, SequenceAsset asset)
        {
            bindingView.Clear();

            if (asset == null)
            {
                bindingView.Add(new Label("Sequence Asset is null.")
                {
                    style = {
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
                    }
                }
            }
        }

        void UpdateOverrideView()
        {
            
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
