#if LITMOTION_SUPPORT_TMP
using System;
using UnityEngine;
using TMPro;

namespace LitMotion.Extensions
{
    internal static class TextMeshProHelper
    {
        public static void SetCharColor(TMP_Text text, int charIndex, Color color)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];
            if (meshInfo.colors32 == null) return;
            for (var i = 0; i < 4; i++)
            {
                meshInfo.colors32[charInfo.vertexIndex + i] = color;
            }
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        public static void SetCharColorR(TMP_Text text, int charIndex, float r)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];
            if (meshInfo.colors32 == null) return;
            var colorSpan = meshInfo.colors32.AsSpan();
            var value = (byte)Mathf.Round(Mathf.Clamp01(r) * 255f);
            for (var i = 0; i < 4; i++)
            {
                colorSpan[charInfo.vertexIndex + i].r = value;
            }
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        public static void SetCharColorG(TMP_Text text, int charIndex, float g)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];
            if (meshInfo.colors32 == null) return;
            var colorSpan = meshInfo.colors32.AsSpan();
            var value = (byte)Mathf.Round(Mathf.Clamp01(g) * 255f);
            for (var i = 0; i < 4; i++)
            {
                colorSpan[charInfo.vertexIndex + i].g = value;
            }
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        public static void SetCharColorB(TMP_Text text, int charIndex, float b)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];
            if (meshInfo.colors32 == null) return;
            var colorSpan = meshInfo.colors32.AsSpan();
            var value = (byte)Mathf.Round(Mathf.Clamp01(b) * 255f);
            for (var i = 0; i < 4; i++)
            {
                colorSpan[charInfo.vertexIndex + i].b = value;
            }
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        public static void SetCharColorA(TMP_Text text, int charIndex, float a)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];
            if (meshInfo.colors32 == null) return;
            var colorSpan = meshInfo.colors32.AsSpan();
            var value = (byte)Mathf.Round(Mathf.Clamp01(a) * 255f);
            for (var i = 0; i < 4; i++)
            {
                colorSpan[charInfo.vertexIndex + i].a = value;
            }
            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
        }

        public static void SetCharPosition(TMP_Text text, int charIndex, Vector3 offset)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];

            if (meshInfo.vertices == null) return;
            var verticesSpan = meshInfo.vertices.AsSpan();

            var initCharPosition = (charInfo.vertex_BL.position + charInfo.vertex_TR.position) * 0.5f;
            var currentCharPosition = (verticesSpan[charInfo.vertexIndex] + verticesSpan[charInfo.vertexIndex + 2]) * 0.5f;
            for (var i = 0; i < 4; i++)
            {
                verticesSpan[charInfo.vertexIndex + i] = initCharPosition + offset + (verticesSpan[charInfo.vertexIndex + i] - currentCharPosition);
            }

            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }

        public static void SetCharRotation(TMP_Text text, int charIndex, Quaternion rotation)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];

            if (meshInfo.vertices == null) return;
            var verticesSpan = meshInfo.vertices.AsSpan();

            var initCharPosition = (charInfo.vertex_BL.position + charInfo.vertex_TR.position) * 0.5f;
            var currentCharPosition = (verticesSpan[charInfo.vertexIndex] + verticesSpan[charInfo.vertexIndex + 2]) * 0.5f;
            for (var i = 0; i < 4; i++)
            {
                var position = i switch
                {
                    0 => charInfo.vertex_BL.position,
                    1 => charInfo.vertex_TL.position,
                    2 => charInfo.vertex_TR.position,
                    3 => charInfo.vertex_BR.position,
                    _ => default
                };
                var dir = rotation * (position - initCharPosition);
                var length = (verticesSpan[charInfo.vertexIndex + i] - currentCharPosition).magnitude;

                verticesSpan[charInfo.vertexIndex + i] = currentCharPosition + dir.normalized * length;
            }

            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }

        public static void SetCharScale(TMP_Text text, int charIndex, Vector3 scale)
        {
            var textInfo = text.textInfo;
            ref var charInfo = ref textInfo.characterInfo.AsSpan()[charIndex];
            ref var meshInfo = ref textInfo.meshInfo.AsSpan()[charInfo.materialReferenceIndex];

            if (meshInfo.vertices == null) return;
            var verticesSpan = meshInfo.vertices.AsSpan();

            var initCharPosition = (charInfo.vertex_BL.position + charInfo.vertex_TR.position) * 0.5f;
            var currentCharPosition = (verticesSpan[charInfo.vertexIndex] + verticesSpan[charInfo.vertexIndex + 2]) * 0.5f;
            for (var i = 0; i < 4; i++)
            {
                var length = i switch
                {
                    0 => (charInfo.vertex_BL.position - initCharPosition).magnitude,
                    1 => (charInfo.vertex_TL.position - initCharPosition).magnitude,
                    2 => (charInfo.vertex_TR.position - initCharPosition).magnitude,
                    3 => (charInfo.vertex_BR.position - initCharPosition).magnitude,
                    _ => default
                };
                var dir = verticesSpan[charInfo.vertexIndex + i] - currentCharPosition;
                var normalizedOffset = dir.normalized * length;

                verticesSpan[charInfo.vertexIndex + i].x = currentCharPosition.x + normalizedOffset.x * scale.x;
                verticesSpan[charInfo.vertexIndex + i].y = currentCharPosition.y + normalizedOffset.y * scale.y;
                verticesSpan[charInfo.vertexIndex + i].z = currentCharPosition.z + normalizedOffset.z * scale.z;
            }

            text.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices);
        }
    }
}
#endif