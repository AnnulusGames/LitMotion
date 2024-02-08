using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;

namespace LitMotion.Sequences.Editor
{
    // TODO: Simplify implementation, separate VisualElements, and optimize

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
            if (target != null) Player.CancelAndRestoreValues();
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

                overrideView.Unbind();
                if (asset != null)
                {
                    SetExposedNames(player, asset.Components);
                }
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

            var bindingsContainer = new IMGUIContainer(() =>
            {
                var table = (IExposedPropertyTable)target;
                var asset = (SequenceAsset)serializedObject.FindProperty("asset").objectReferenceValue;

                if (asset == null || asset.Components.Count == 0)
                {
                    EditorGUILayout.LabelField("Empty");
                    return;
                }

                DrawBindingFields(asset.Components);
            });
            bindingView.Add(bindingsContainer);

            // Motions -----------------------------------------------------------
            overrideView = new VisualElement();
            root.Add(overrideView);

            // Debug -----------------------------------------------------------
            root.Add(new BoldLabel("Debug"));

            var playButton = new IMGUIContainer(() =>
            {
                using (new EditorGUI.DisabledScope(assetProperty.objectReferenceValue == null))
                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Play")) Player.PlayPreview();
                    using (new EditorGUI.DisabledScope(!Player.IsModified))
                    {
                        if (GUILayout.Button("Stop")) Player.CancelAndRestoreValues();
                    }
                }
            })
            {
                style = {
                    marginLeft = 4f
                }
            };
            root.Add(playButton);

            UpdateOverrideView(assetProperty);

            root.schedule.Execute(() =>
            {
                var isModified = Player.IsModified;
                assetField.SetEnabled(!isModified);
                bindingView.SetEnabled(!isModified);
                overrideView.SetEnabled(!isModified);
            })
            .Every(50);

            return root;
        }
        void DrawBindingFields(IEnumerable<SequenceComponent> components)
        {
            if (target == null) return;
            var table = (IExposedPropertyTable)target;

            foreach (var component in components)
            {
                if (component == null) continue;

                var serializedObject = new SerializedObject(component, target);
                var iterator = serializedObject.GetIterator();
                while (iterator.NextVisible(true))
                {
                    if (iterator.propertyType == SerializedPropertyType.ExposedReference)
                    {
                        var property = iterator;
                        var label = component.displayName + " / " + property.displayName;

                        var obj = EditorGUILayout.ObjectField(label, property.exposedReferenceValue, property.GetPropertyType().GenericTypeArguments[0], true);
                        if (obj != property.exposedReferenceValue)
                        {
                            property.exposedReferenceValue = obj;
                            serializedObject.ApplyModifiedProperties();
                            SetExposedNames(table, components);
                        }
                    }
                }
            }
        }

        void UpdateOverrideView(SerializedProperty assetProperty)
        {
            overrideView.Clear();
            if (assetProperty.objectReferenceValue == null) return;

            var view = new SequenceAssetView(new SerializedObject(assetProperty.objectReferenceValue));
            view.style.marginLeft = 2f;
            view.style.marginTop = 3f;
            overrideView.Add(view);
        }

        void SetExposedNames(IExposedPropertyTable table, IEnumerable<SequenceComponent> components)
        {
            foreach (var component in components)
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
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
