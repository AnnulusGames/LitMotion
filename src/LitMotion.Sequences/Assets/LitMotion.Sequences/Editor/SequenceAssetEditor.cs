using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    [CustomEditor(typeof(SequenceAsset))]
    public sealed class SequenceAssetEditor : UnityEditor.Editor
    {
        static readonly Dictionary<UnityEngine.Object, VisualElement> inspectorCache = new();
        static VisualElement GetOrCreateInspector(UnityEngine.Object key)
        {
            if (!inspectorCache.TryGetValue(key, out var element))
            {
                element = CreateEditor(key).CreateInspectorGUI();
                element.Bind(new SerializedObject(key));
            }

            return element;
        }

        static readonly string LightStyleSheetGUID = "e0272c41884fc453e86f6260dd9a0eae";
        static readonly string DarkStyleSheetGUID = "a298adbec4db64c3997962890f9f359e";

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();

            root.Add(new Label("Settings")
            {
                style = {
                    unityFontStyleAndWeight = FontStyle.Bold,
                    fontSize = 12.5f,
                    marginTop = 5f, marginBottom = 2f
                },
            });

            var componentsProperty = serializedObject.FindProperty("components");
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath(
                EditorGUIUtility.isProSkin ? DarkStyleSheetGUID : LightStyleSheetGUID
            ));

            var listView = new ListView
            {
                showBorder = false,
                showFoldoutHeader = false,
                showBoundCollectionSize = false,
                reorderable = true,
                reorderMode = ListViewReorderMode.Animated,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                makeItem = () => new VisualElement(),
                bindItem = (element, index) =>
                {
                    var elementProperty = componentsProperty.GetArrayElementAtIndex(index);
                    element.Add(GetOrCreateInspector(elementProperty.objectReferenceValue));
                },
                unbindItem = (element, index) =>
                {
                    element.Clear();
                },
                style = {
                    marginLeft = -15f,
                    marginRight = -5f,
                },
                bindingPath = "components",
            };
            listView.Bind(serializedObject);
            listView.styleSheets.Add(styleSheet);
            root.Add(listView);

            return root;
        }
    }
}
