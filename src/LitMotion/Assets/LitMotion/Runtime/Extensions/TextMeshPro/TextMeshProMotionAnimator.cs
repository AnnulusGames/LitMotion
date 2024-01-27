#if LITMOTION_SUPPORT_TMP
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;

namespace LitMotion.Extensions
{
    // TODO: optimization

    internal sealed class TextMeshProMotionAnimator
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init()
        {
            PlayerLoopHelper.OnUpdate += UpdateActiveAnimators;
        }

        static TextMeshProMotionAnimator rootNode;

        public static TextMeshProMotionAnimator Get(TMP_Text text)
        {
            if (textToAnimator.TryGetValue(text, out var animator)) return animator;

            // get or create animator
            animator = rootNode ?? new();
            rootNode = animator.nextNode;
            animator.nextNode = null;

            // set target
            animator.target = text;

            // add to array
            if (tail == animators.Length)
            {
                Array.Resize(ref animators, tail * 2);
            }
            animators[tail] = animator;
            tail++;

            // add to dictionary
            textToAnimator.Add(text, animator);

            return animator;
        }

        public static void Return(TextMeshProMotionAnimator animator)
        {
            animator.nextNode = rootNode;
            rootNode = animator;

            textToAnimator.Remove(animator.target);
            animator.target = null;
        }

        static readonly Dictionary<TMP_Text, TextMeshProMotionAnimator> textToAnimator = new();

        static TextMeshProMotionAnimator[] animators = new TextMeshProMotionAnimator[8];
        static int tail;

        public static void UpdateActiveAnimators()
        {
            var j = tail - 1;

            for (int i = 0; i < animators.Length; i++)
            {
                var animator = animators[i];
                if (animator != null)
                {
                    if (!animator.TryUpdate())
                    {
                        Return(animator);
                        animators[i] = null;
                    }
                    else
                    {
                        continue;
                    }
                }

                while (i < j)
                {
                    var fromTail = animators[j];
                    if (fromTail != null)
                    {
                        if (!fromTail.TryUpdate())
                        {
                            Return(fromTail);
                            animators[j] = null;
                            j--;
                            continue;
                        }
                        else
                        {
                            animators[i] = fromTail;
                            animators[j] = null;
                            j--;
                            goto NEXT_LOOP;
                        }
                    }
                    else
                    {
                        j--;
                    }
                }

                tail = i;
                break;

            NEXT_LOOP:
                continue;
            }
        }

        public struct CharInfo
        {
            public Vector3 offset;
            public Vector3 scale;
            public Quaternion rotation;
            public Color color;
        }

        TMP_Text target;
        public CharInfo[] charInfoArray = new CharInfo[32];
        public MinimumList<MotionHandle> motionHandleList = new();

        TextMeshProMotionAnimator nextNode;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void EnsureCapacity(int length)
        {
            var prevLength = charInfoArray.Length;
            if (length > prevLength)
            {
                Array.Resize(ref charInfoArray, length);

                if (length > prevLength)
                {
                    for (int i = prevLength; i < length; i++)
                    {
                        charInfoArray[i].color = new(target.color.r, target.color.g, target.color.b, target.color.a);
                        charInfoArray[i].rotation = Quaternion.identity;
                        charInfoArray[i].scale = Vector3.one;
                        charInfoArray[i].offset = Vector3.zero;
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        bool TryUpdate()
        {
            if (target == null) return false;

            for (int i = 0; i < motionHandleList.Length; i++)
            {
                if (!motionHandleList[i].IsActive())
                {
                    motionHandleList.RemoveAtSwapback(i);
                    i--;
                }
            }

            if (motionHandleList.Length == 0) return false;

            target.ForceMeshUpdate();

            var textInfo = target.textInfo;
            EnsureCapacity(textInfo.characterCount);

            for (int i = 0; i < textInfo.characterCount; i++)
            {
                ref var charInfo = ref textInfo.characterInfo[i];
                if (!charInfo.isVisible) continue;

                var materialIndex = charInfo.materialReferenceIndex;
                var vertexIndex = charInfo.vertexIndex;

                ref var colors = ref textInfo.meshInfo[materialIndex].colors32;
                ref var motionCharInfo = ref charInfoArray[i];

                var charColor = motionCharInfo.color;
                for (int n = 0; n < 4; n++)
                {
                    colors[vertexIndex + n] = charColor;
                }

                var verts = textInfo.meshInfo[materialIndex].vertices;
                var center = (verts[vertexIndex] + verts[vertexIndex + 2]) * 0.5f;

                var charRotation = motionCharInfo.rotation;
                var charScale = motionCharInfo.scale;
                var charOffset = motionCharInfo.offset;
                for (int n = 0; n < 4; n++)
                {
                    var vert = verts[vertexIndex + n];
                    var dir = vert - center;
                    verts[vertexIndex + n] = center +
                        charRotation * new Vector3(dir.x * charScale.x, dir.y * charScale.y, dir.z * charScale.z) +
                        charOffset;
                }
            }

            for (int i = 0; i < textInfo.materialCount; i++)
            {
                if (textInfo.meshInfo[i].mesh == null) continue;
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                textInfo.meshInfo[i].mesh.vertices = textInfo.meshInfo[i].vertices;
                target.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            return true;
        }
    }
}
#endif