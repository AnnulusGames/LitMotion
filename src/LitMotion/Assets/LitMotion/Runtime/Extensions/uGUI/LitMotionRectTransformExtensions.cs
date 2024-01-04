using UnityEngine;

namespace LitMotion.Extensions
{
    public static class LitMotionRectTransformExtensions
    {
        public static MotionHandle BindToAnchoredPosition<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchoredPosition = x;
            });
        }

        public static MotionHandle BindToAnchoredPositionX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition;
                p.x = x;
                target.anchoredPosition = p;
            });
        }

        public static MotionHandle BindToAnchoredPositionY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition;
                p.y = x;
                target.anchoredPosition = p;
            });
        }

        public static MotionHandle BindToAnchoredPosition3D<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchoredPosition3D = x;
            });
        }

        public static MotionHandle BindToAnchoredPosition3DX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition3D;
                p.x = x;
                target.anchoredPosition3D = p;
            });
        }

        public static MotionHandle BindToAnchoredPosition3DY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition3D;
                p.y = x;
                target.anchoredPosition3D = p;
            });
        }

        public static MotionHandle BindToAnchoredPosition3DZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var p = target.anchoredPosition3D;
                p.z = x;
                target.anchoredPosition3D = p;
            });
        }

        public static MotionHandle BindToAnchorMin<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchorMin = x;
            });
        }

        public static MotionHandle BindToAnchorMax<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.anchorMax = x;
            });
        }


        public static MotionHandle BindToSizeDelta<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.sizeDelta = x;
            });
        }

        public static MotionHandle BindToSizeDeltaX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.sizeDelta;
                s.x = x;
                target.sizeDelta = s;
            });
        }

        public static MotionHandle BindToSizeDeltaY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.sizeDelta;
                s.y = x;
                target.sizeDelta = s;
            });
        }

        public static MotionHandle BindToPivot<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                target.pivot = x;
            });
        }

        public static MotionHandle BindToPivotX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.BindWithState(rectTransform, (x, target) =>
            {
                if (target == null) return;
                var s = target.pivot;
                s.x = x;
                target.pivot = s;
            });
        }

        public static MotionHandle BindToPivotY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
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