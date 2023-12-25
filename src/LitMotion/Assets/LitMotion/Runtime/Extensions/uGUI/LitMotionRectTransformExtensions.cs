using UnityEngine;
using UnityEngine.Assertions;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionRectTransformExtensions
    {
        public static MotionHandle BindToAnchoredPosition(this MotionBuilder<Vector2, NoOptions, Vector2MotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchoredPosition = x;
            });
        }

        public static MotionHandle BindToAnchoredPositionX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition;
                p.x = x;
                target.anchoredPosition = p;
            });
        }

        public static MotionHandle BindToAnchoredPositionY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition;
                p.y = x;
                target.anchoredPosition = p;
            });
        }

        public static MotionHandle BindToAnchoredPosition3D(this MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchoredPosition3D = x;
            });
        }

        public static MotionHandle BindToAnchoredPosition3DX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition3D;
                p.x = x;
                target.anchoredPosition3D = p;
            });
        }

        public static MotionHandle BindToAnchoredPosition3DY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition3D;
                p.y = x;
                target.anchoredPosition3D = p;
            });
        }

        public static MotionHandle BindToAnchoredPosition3DZ(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition3D;
                p.z = x;
                target.anchoredPosition3D = p;
            });
        }

        public static MotionHandle BindToAnchorMin(this MotionBuilder<Vector2, NoOptions, Vector2MotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchorMin = x;
            });
        }

        public static MotionHandle BindToAnchorMax(this MotionBuilder<Vector2, NoOptions, Vector2MotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchorMax = x;
            });
        }


        public static MotionHandle BindToSizeDelta(this MotionBuilder<Vector2, NoOptions, Vector2MotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.sizeDelta = x;
            });
        }

        public static MotionHandle BindToSizeDeltaX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.sizeDelta;
                s.x = x;
                target.sizeDelta = s;
            });
        }

        public static MotionHandle BindToSizeDeltaY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.sizeDelta;
                s.y = x;
                target.sizeDelta = s;
            });
        }

        public static MotionHandle BindToPivot(this MotionBuilder<Vector2, NoOptions, Vector2MotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.pivot = x;
            });
        }

        public static MotionHandle BindToPivotX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.pivot;
                s.x = x;
                target.pivot= s;
            });
        }

        public static MotionHandle BindToPivotY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, RectTransform rectTransform)
        {
            Assert.IsNotNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.pivot;
                s.y = x;
                target.pivot = s;
            });
        }
    }
}