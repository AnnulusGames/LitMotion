using UnityEngine;
using UnityEngine.Assertions;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionTransformExtensions
    {
        public static MotionHandle BindToPosition(this MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.position = x;
            });
        }

        public static MotionHandle BindToPositionX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.position;
                p.x = x;
                t.position = p;
            });
        }

        public static MotionHandle BindToPositionY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.position;
                p.y = x;
                t.position = p;
            });
        }

        public static MotionHandle BindToPositionZ(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.position;
                p.z = x;
                t.position = p;
            });
        }

        public static MotionHandle BindToLocalPosition(this MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localPosition = x;
            });
        }

        public static MotionHandle BindToLocalPositionX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localPosition;
                p.x = x;
                t.localPosition = p;
            });
        }

        public static MotionHandle BindToLocalPositionY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localPosition;
                p.y = x;
                t.localPosition = p;
            });
        }

        public static MotionHandle BindToLocalPositionZ(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localPosition;
                p.z = x;
                t.localPosition = p;
            });
        }

        public static MotionHandle BindToRotation(this MotionBuilder<Quaternion, NoOptions, QuaternionMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.rotation = x;
            });
        }

        public static MotionHandle BindToLocalRotation(this MotionBuilder<Quaternion, NoOptions, QuaternionMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localRotation = x;
            });
        }

        public static MotionHandle BindToEulerAngles(this MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.eulerAngles = x;
            });
        }

        public static MotionHandle BindToEulerAnglesX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.eulerAngles;
                p.x = x;
                t.eulerAngles = p;
            });
        }

        public static MotionHandle BindToEulerAnglesY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.eulerAngles;
                p.y = x;
                t.eulerAngles = p;
            });
        }

        public static MotionHandle BindToEulerAnglesZ(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.eulerAngles;
                p.z = x;
                t.eulerAngles = p;
            });
        }

        public static MotionHandle BindToLocalEulerAngles(this MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localEulerAngles = x;
            });
        }

        public static MotionHandle BindToLocalEulerAnglesX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localEulerAngles;
                p.x = x;
                t.localEulerAngles = p;
            });
        }

        public static MotionHandle BindToLocalEulerAnglesY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localEulerAngles;
                p.y = x;
                t.localEulerAngles = p;
            });
        }

        public static MotionHandle BindToLocalEulerAnglesZ(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localEulerAngles;
                p.z = x;
                t.localEulerAngles = p;
            });
        }

        public static MotionHandle BindToLocalScale(this MotionBuilder<Vector3, NoOptions, Vector3MotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                t.localScale = x;
            });
        }

        public static MotionHandle BindToLocalScaleX(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localScale;
                p.x = x;
                t.localScale = p;
            });
        }

        public static MotionHandle BindToLocalScaleY(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
            return builder.BindWithState(transform, (x, t) =>
            {
                if (t == null) return;
                var p = t.localScale;
                p.y = x;
                t.localScale = p;
            });
        }

        public static MotionHandle BindToLocalScaleZ(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, Transform transform)
        {
            Assert.IsNotNull(transform);
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