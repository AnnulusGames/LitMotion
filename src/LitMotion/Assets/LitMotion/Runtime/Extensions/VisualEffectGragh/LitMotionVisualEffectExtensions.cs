#if LITMOTION_SUPPORT_VFX_GRAPH
using UnityEngine;
using UnityEngine.VFX;

namespace LitMotion.Extensions
{
    public static class LitMotionVisualEffectExtensions
    {
        public static MotionHandle BindToVisualEffectFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetFloat(name, x);
            });
        }

        public static MotionHandle BindToVisualEffectFloat<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetFloat(nameID, x);
            });
        }

        public static MotionHandle BindToVisualEffectInt<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetInt(name, x);
            });
        }

        public static MotionHandle BindToVisualEffectInt<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetFloat(nameID, x);
            });
        }

        public static MotionHandle BindToVisualEffectVector2<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetVector2(name, x);
            });
        }

        public static MotionHandle BindToVisualEffectVector2<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetVector2(nameID, x);
            });
        }

        public static MotionHandle BindToVisualEffectVector3<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetVector3(name, x);
            });
        }

        public static MotionHandle BindToVisualEffectVector3<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetVector3(nameID, x);
            });
        }

        public static MotionHandle BindToVisualEffectVector4<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, VisualEffect visualEffect, string name)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetVector4(name, x);
            });
        }

        public static MotionHandle BindToVisualEffectVector4<TOptions, TAdapter>(this MotionBuilder<Vector4, TOptions, TAdapter> builder, VisualEffect visualEffect, int nameID)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector4, TOptions>
        {
            Error.IsNull(visualEffect);
            return builder.BindWithState(visualEffect, (x, target) =>
            {
                if (target == null) return;
                target.SetVector4(nameID, x);
            });
        }
    }
}
#endif