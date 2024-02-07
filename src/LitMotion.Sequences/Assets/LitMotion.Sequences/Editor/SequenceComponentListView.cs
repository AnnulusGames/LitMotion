using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Sequences.Editor
{
    public sealed class SequenceComponentListView : VisualElement
    {
        static readonly Dictionary<Object, VisualElement> inspectorCache = new();
        static VisualElement GetOrCreateInspector(Object key)
        {
            if (!inspectorCache.TryGetValue(key, out var element))
            {
                element = UnityEditor.Editor.CreateEditor(key).CreateInspectorGUI();
                element.Bind(new SerializedObject(key));
            }

            return element;
        }

        static readonly string LightStyleSheetGUID = "e0272c41884fc453e86f6260dd9a0eae";
        static readonly string DarkStyleSheetGUID = "a298adbec4db64c3997962890f9f359e";

        public SequenceComponentListView(SerializedObject serializedObject)
        {
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

            Add(listView);

            var button = new Button(() =>
            {

            })
            {
                text = "Add Motion",
                style = {
                    alignSelf = Align.Center,
                    width = 220f,
                    height = 22f,
                    marginTop = 5f, marginBottom = 5f
                }
            };

            Add(button);
        }
    }
}
