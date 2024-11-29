using UnityEngine;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for RectTransform.
    /// </summary>
    public static class LitMotionRectTransformExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPosition<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                target.anchoredPosition = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPositionX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var p = target.anchoredPosition;
                p.x = x;
                target.anchoredPosition = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPositionY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var p = target.anchoredPosition;
                p.y = x;
                target.anchoredPosition = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition3D
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPosition3D<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                target.anchoredPosition3D = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition3D.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPosition3DX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var p = target.anchoredPosition3D;
                p.x = x;
                target.anchoredPosition3D = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition3D.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPosition3DY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var p = target.anchoredPosition3D;
                p.y = x;
                target.anchoredPosition3D = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchoredPosition3D.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchoredPosition3DZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var p = target.anchoredPosition3D;
                p.z = x;
                target.anchoredPosition3D = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchorMin
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchorMin<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                target.anchorMin = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.anchorMax
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToAnchorMax<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                target.anchorMax = x;
            });
        }


        /// <summary>
        /// Create a motion data and bind it to RectTransform.sizeDelta
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToSizeDelta<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                target.sizeDelta = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.sizeDelta.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToSizeDeltaX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var s = target.sizeDelta;
                s.x = x;
                target.sizeDelta = s;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.sizeDelta.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToSizeDeltaY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var s = target.sizeDelta;
                s.y = x;
                target.sizeDelta = s;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.pivot
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPivot<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                target.pivot = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.pivot.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPivotX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var s = target.pivot;
                s.x = x;
                target.pivot = s;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to RectTransform.pivot.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPivotY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, RectTransform rectTransform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(rectTransform);
            return builder.Bind(rectTransform, static (x, target) =>
            {
                var s = target.pivot;
                s.y = x;
                target.pivot = s;
            });
        }
    }
}