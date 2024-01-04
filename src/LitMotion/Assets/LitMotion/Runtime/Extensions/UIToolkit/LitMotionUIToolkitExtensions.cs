using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace LitMotion.Extensions
{
    public static class LitMotionUIToolkitExtensions
    {
        #region VisualElement

        public static MotionHandle BindToStyleLeft<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.left = x;
            });
        }

        public static MotionHandle BindToStyleRight<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.right = x;
            });
        }

        public static MotionHandle BindToStyleTop<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.top = x;
            });
        }

        public static MotionHandle BindToStyleBottom<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.bottom = x;
            });
        }

        public static MotionHandle BindToStyleWidth<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.width = x;
            });
        }

        public static MotionHandle BindToStyleHeight<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.height = x;
            });
        }

        public static MotionHandle BindToStyleColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.color = x;
            });
        }

        public static MotionHandle BindToStyleColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.color.value;
                c.r = x;
                target.style.color = c;
            });
        }

        public static MotionHandle BindToStyleColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.color.value;
                c.g = x;
                target.style.color = c;
            });
        }

        public static MotionHandle BindToStyleColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.color.value;
                c.b = x;
                target.style.color = c;
            });
        }

        public static MotionHandle BindToStyleColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.color.value;
                c.a = x;
                target.style.color = c;
            });
        }

        public static MotionHandle BindToStyleBackgroundColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.backgroundColor = x;
            });
        }

        public static MotionHandle BindToStyleBackgroundColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.backgroundColor.value;
                c.r = x;
                target.style.backgroundColor = c;
            });
        }

        public static MotionHandle BindToStyleBackgroundColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.backgroundColor.value;
                c.g = x;
                target.style.backgroundColor = c;
            });
        }

        public static MotionHandle BindToStyleBackgroundColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.backgroundColor.value;
                c.b = x;
                target.style.backgroundColor = c;
            });
        }

        public static MotionHandle BindToStyleBackgroundColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                var c = target.style.backgroundColor.value;
                c.a = x;
                target.style.backgroundColor = c;
            });
        }

        public static MotionHandle BindToStyleOpacity<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.opacity = x;
            });
        }

        public static MotionHandle BindToStyleFontSize<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.fontSize = x;
            });
        }

        public static MotionHandle BindToStyleWordSpacing<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.wordSpacing = x;
            });
        }

        public static MotionHandle BindToStyleTranslate<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.translate = new Translate(x.x, x.y, x.z);
            });
        }

        public static MotionHandle BindToStyleTranslate<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.translate = new Translate(x.x, x.y);
            });
        }

        public static MotionHandle BindToStyleRotate<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement, AngleUnit angleUnit = AngleUnit.Degree)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.rotate = new Rotate(new Angle(x, angleUnit));
            });
        }

        public static MotionHandle BindToStyleScale<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.scale = new Scale(x);
            });
        }

        public static MotionHandle BindToStyleTransformOrigin<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.transformOrigin = new TransformOrigin(x.x, x.y, x.z);
            });
        }

        public static MotionHandle BindToStyleTransformOrigin<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.BindWithState(visualElement, (x, target) =>
            {
                if (target == null) return;
                target.style.transformOrigin = new TransformOrigin(x.x, x.y);
            });
        }

        #endregion

        #region AbstractProgressBar

        public static MotionHandle BindToProgressBar<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AbstractProgressBar progressBar)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(progressBar);
            return builder.BindWithState(progressBar, (x, target) =>
            {
                if (target == null) return;
                target.value = x;
            });
        }

        #endregion

        #region TextElement

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString32Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString32Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.BindWithState(textElement, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString64Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString64Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.BindWithState(textElement, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString128Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString128Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.BindWithState(textElement, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString512Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString512Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.BindWithState(textElement, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString4096Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString4096Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.BindWithState(textElement, (x, target) =>
            {
                if (target == null) return;
                target.text = x.ConvertToString();
            });
        }

        #endregion
    }
}