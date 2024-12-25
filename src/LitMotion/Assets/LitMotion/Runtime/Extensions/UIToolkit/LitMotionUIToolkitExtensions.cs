#if LITMOTION_SUPPORT_UIELEMENTS
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;
#if LITMOTION_SUPPORT_ZSTRING
using Cysharp.Text;
#endif

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for UIElements.
    /// </summary>
    public static class LitMotionUIToolkitExtensions
    {
        #region VisualElement

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.left
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleLeft<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.left = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.right
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleRight<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.right = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.top
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleTop<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.top = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.bottom
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleBottom<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.bottom = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.width
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleWidth<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.width = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.height
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleHeight<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.height = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.color
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.color = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.color.r
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.color.value;
                c.r = x;
                target.style.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.color.g
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.color.value;
                c.g = x;
                target.style.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.color.b
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.color.value;
                c.b = x;
                target.style.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.color.a
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.color.value;
                c.a = x;
                target.style.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.backgroundColor
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleBackgroundColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.backgroundColor = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.backgroundColor.r
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleBackgroundColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.backgroundColor.value;
                c.r = x;
                target.style.backgroundColor = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.backgroundColor.g
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleBackgroundColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.backgroundColor.value;
                c.g = x;
                target.style.backgroundColor = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.backgroundColor.b
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleBackgroundColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.backgroundColor.value;
                c.b = x;
                target.style.backgroundColor = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.backgroundColor.a
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleBackgroundColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                var c = target.style.backgroundColor.value;
                c.a = x;
                target.style.backgroundColor = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.opacity
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleOpacity<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.opacity = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.fontSize
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleFontSize<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.fontSize = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.wordSpacing
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleWordSpacing<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.wordSpacing = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.translate
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleTranslate<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.translate = new Translate(x.x, x.y, x.z);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.translate
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleTranslate<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.translate = new Translate(x.x, x.y);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.rotate
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleRotate<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, VisualElement visualElement, AngleUnit angleUnit = AngleUnit.Degree)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, (x, target) =>
            {
                target.style.rotate = new Rotate(new Angle(x, angleUnit));
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.scale
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleScale<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.scale = new Scale(x);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.transformOrigin
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleTransformOrigin<TOptions, TAdapter>(this MotionBuilder<Vector3, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector3, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.transformOrigin = new TransformOrigin(x.x, x.y, x.z);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to VisualElement.style.transformOrigin
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToStyleTransformOrigin<TOptions, TAdapter>(this MotionBuilder<Vector2, TOptions, TAdapter> builder, VisualElement visualElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Vector2, TOptions>
        {
            Error.IsNull(visualElement);
            return builder.Bind(visualElement, static (x, target) =>
            {
                target.style.transformOrigin = new TransformOrigin(x.x, x.y);
            });
        }

        #endregion

        #region AbstractProgressBar

        /// <summary>
        /// Create a motion data and bind it to AbstractProgressBar.value
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToProgressBar<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, AbstractProgressBar progressBar)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(progressBar);
            return builder.Bind(progressBar, static (x, target) =>
            {
                target.value = x;
            });
        }

        #endregion

        #region TextElement

        /// <summary>
        /// Create a motion data and bind it to TextElement.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString32Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString32Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ConvertToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TextElement.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString64Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString64Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ConvertToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TextElement.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString128Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString128Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ConvertToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TextElement.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString512Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString512Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ConvertToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TextElement.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString4096Bytes, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString4096Bytes, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ConvertToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Text.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Text.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <param name="format">Format string</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TextElement textElement, string format)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, format, static (x, textElement, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                textElement.text = ZString.Format(format, x);
#else
                textElement.text = string.Format(format, x);
#endif
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Text.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<long, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<long, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Text.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <param name="format">Format string</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<long, TOptions, TAdapter> builder, TextElement textElement, string format)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<long, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, format, static (x, textElement, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                textElement.text = ZString.Format(format, x);
#else
                textElement.text = string.Format(format, x);
#endif
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Text.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TextElement textElement)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, static (x, target) =>
            {
                target.text = x.ToString();
            });
        }

        /// <summary>
        /// Create a motion data and bind it to Text.text
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="transform"></param>
        /// <param name="format">Format string</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TextElement textElement, string format)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(textElement);
            return builder.Bind(textElement, format, static (x, textElement, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                textElement.text = ZString.Format(format, x);
#else
                textElement.text = string.Format(format, x);
#endif
            });
        }
        #endregion
    }
}
#endif
