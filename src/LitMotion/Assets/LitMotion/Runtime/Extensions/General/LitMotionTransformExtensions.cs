using UnityEngine;

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for Transform.
    /// </summary>
    public static class LitMotionTransformExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to Transform.position
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPosition<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.position = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.position.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.position;
                p.x = x;
                t.position = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.position.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.position;
                p.y = x;
                t.position = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.position.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToPositionZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.position;
                p.z = x;
                t.position = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localPosition
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalPosition<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localPosition = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localPosition.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalPositionX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localPosition;
                p.x = x;
                t.localPosition = p;
            });
        }


        /// <summary>
        /// Create a motion data and bind it to Transform.localPosition.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalPositionY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localPosition;
                p.y = x;
                t.localPosition = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localPosition.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalPositionZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localPosition;
                p.z = x;
                t.localPosition = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.rotation
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToRotation<TOptions, TAdapter>(this MotionBuilder<Quaternion, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Quaternion, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.rotation = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localRotation
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalRotation<TOptions, TAdapter>(this MotionBuilder<Quaternion, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Quaternion, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localRotation = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.eulerAngles
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToEulerAngles<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.eulerAngles = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.eulerAngles.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToEulerAnglesX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.eulerAngles;
                p.x = x;
                t.eulerAngles = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.eulerAngles.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToEulerAnglesY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.eulerAngles;
                p.y = x;
                t.eulerAngles = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.eulerAngles.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToEulerAnglesZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.eulerAngles;
                p.z = x;
                t.eulerAngles = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localEulerAngles
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalEulerAngles<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localEulerAngles = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localEulerAngles.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalEulerAnglesX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localEulerAngles;
                p.x = x;
                t.localEulerAngles = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localEulerAngles.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalEulerAnglesY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localEulerAngles;
                p.y = x;
                t.localEulerAngles = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localEulerAngles.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalEulerAnglesZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localEulerAngles;
                p.z = x;
                t.localEulerAngles = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localScale
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalScale<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localScale = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localScale.x
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalScaleX<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localScale;
                p.x = x;
                t.localScale = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localScale.y
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalScaleY<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localScale;
                p.y = x;
                t.localScale = p;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Transform.localScale.z
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToLocalScaleZ<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Transform transform)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localScale;
                p.z = x;
                t.localScale = p;
            });
        }
    }
}