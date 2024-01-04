using UnityEngine;
using UnityEngine.Assertions;

namespace LitMotion.Extensions
{
    public static class LitMotionTransformExtensions
    {
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