using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequencePlayer))]
    public sealed class SequencePlayerEditor : UnityEditor.Editor
    {
        internal sealed class BoldLabel : Label
        {
            public BoldLabel(string text) : base(text)
            {
                style.unityFontStyleAndWeight = FontStyle.Bold;
                style.fontSize = 12f;
                style.marginLeft = 4f;
                style.marginTop = 3f;
                style.marginBottom = 3f;
            }
        }

        VisualElement bindingView;
        VisualElement overrideView;

        static readonly string ExposedNamePropertyName = "exposedName";

        SequencePlayer Player => (SequencePlayer)target;

        void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        void OnDisable()
        {
            Player.CancelAndRestoreValues();
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.ExitingEditMode)
            {
                Player.CancelAndRestoreValues();
            }
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            // Asset -----------------------------------------------------------
            var assetProperty = serializedObject.FindProperty("asset");
            var assetField = new PropertyField(assetProperty);
            root.Add(assetField);

            assetField.RegisterValueChangeCallback(eventData =>
            {
                var property = eventData.changedProperty;

                var player = (SequencePlayer)property.serializedObject.targetObject;
                var asset = (SequenceAsset)property.objectReferenceValue;

                if (asset != null) SetExposedNames(player, asset);
                UpdateBindingView();
                UpdateOverrideView(property);
            });

            // Bindings -----------------------------------------------------------
            root.Add(new BoldLabel("Bindings"));

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

            // Debug -----------------------------------------------------------
            root.Add(new BoldLabel("Debug"));

            var playButton = new IMGUIContainer(() =>
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Play")) Player.PlayPreview();
                    if (GUILayout.Button("Reset")) Player.CancelAndRestoreValues();
                }
            });
            root.Add(playButton);

            UpdateBindingView();
            UpdateOverrideView(assetProperty);

            // TODO:
            // tmp impl
            root.schedule.Execute(() =>
            {
                var isModified = Player.IsModified;
                assetField.SetEnabled(!isModified);
                bindingView.SetEnabled(!isModified);
                overrideView.SetEnabled(!isModified);
            })
            .Every(20);

            return root;
        }

        void UpdateBindingView()
        {
            bindingView.Clear();

            var table = (IExposedPropertyTable)target;
            var asset = (SequenceAsset)serializedObject.FindProperty("asset").objectReferenceValue;

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
                            UpdateBindingView();
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
            listView.OnOrdered += () => UpdateBindingView();
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
