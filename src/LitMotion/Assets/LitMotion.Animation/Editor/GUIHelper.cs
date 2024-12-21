using System;
using UnityEditor;
using UnityEngine;

namespace LitMotion.Animation.Editor
{
    internal static class GUIHelper
    {
        public static Texture2D GetComponentIcon(Type type)
        {
            // HACK: AssetPreview.GetMiniTypeThumbnail doesn't work for TMP_Text, so set it separately.
            if (type.FullName == "TMPro.TMP_Text")
            {
                return AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.unity.ugui/Editor Resources/Gizmos/TMP - Text Component Icon.psd");
            }
            else
            {
                return AssetPreview.GetMiniTypeThumbnail(type);
            }
        }
    }
}