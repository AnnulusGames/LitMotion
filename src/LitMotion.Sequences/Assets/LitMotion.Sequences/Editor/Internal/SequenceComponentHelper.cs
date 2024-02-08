using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace LitMotion.Sequences.Editor
{
    internal static class SequenceComponentHelper
    {
        public static SequenceComponent CreateAndAddTo(SequenceAsset asset, Type componentType)
        {
            Assert.IsTrue(componentType.IsSubclassOf(typeof(SequenceComponent)));

            var component = ScriptableObject.CreateInstance(componentType);
            component.hideFlags = HideFlags.HideInInspector | HideFlags.HideInHierarchy;
            component.name = componentType.Name;
            var path = AssetDatabase.GetAssetPath(asset);
            if (EditorUtility.IsPersistent(asset))
            {
                AssetDatabase.AddObjectToAsset(component, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.ImportAsset(path);
            }

            return (SequenceComponent)component;
        }
    }
}
