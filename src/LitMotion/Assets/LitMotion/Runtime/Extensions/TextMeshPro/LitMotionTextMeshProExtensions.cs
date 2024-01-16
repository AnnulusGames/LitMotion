#if LITMOTION_SUPPORT_TMP
using System.Buffers;
using UnityEngine;
using Unity.Collections;
using TMPro;
#if LITMOTION_SUPPORT_ZSTRING
using Cysharp.Text;
#endif

namespace LitMotion.Extensions
{
    /// <summary>
    /// Provides binding extension methods for TMP_Text
    /// </summary>
    public static class LitMotionTextMeshProExtensions
    {
        /// <summary>
        /// Create a motion data and bind it to TMP_Text.fontSize
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToFontSize<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                target.fontSize = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.maxVisibleCharacters
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaxVisibleCharacters<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                target.maxVisibleCharacters = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.maxVisibleLines
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaxVisibleLines<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                target.maxVisibleLines = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.maxVisibleWords
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToMaxVisibleWords<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                target.maxVisibleWords = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.color
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                target.color = x;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.color.r
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var c = target.color;
                c.r = x;
                target.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.color.g
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var c = target.color;
                c.g = x;
                target.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.color.b
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var c = target.color;
                c.b = x;
                target.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.color.a
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public static MotionHandle BindToColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var c = target.color;
                c.a = x;
                target.color = c;
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString32Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString32Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var enumerator = x.GetEnumerator();
                var length = 0;
                var buffer = ArrayPool<char>.Shared.Rent(64);
                fixed (char* c = buffer)
                {
                    Unicode.Utf8ToUtf16(x.GetUnsafePtr(), x.Length, c, out length, x.Length * 2);
                }
                target.SetText(buffer, 0, length);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString64Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString64Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var enumerator = x.GetEnumerator();
                var length = 0;
                var buffer = ArrayPool<char>.Shared.Rent(128);
                fixed (char* c = buffer)
                {
                    Unicode.Utf8ToUtf16(x.GetUnsafePtr(), x.Length, c, out length, x.Length * 2);
                }
                target.SetText(buffer, 0, length);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString128Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString128Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var enumerator = x.GetEnumerator();
                var length = 0;
                var buffer = ArrayPool<char>.Shared.Rent(256);
                fixed (char* c = buffer)
                {
                    Unicode.Utf8ToUtf16(x.GetUnsafePtr(), x.Length, c, out length, x.Length * 2);
                }
                target.SetText(buffer, 0, length);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString512Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString512Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var enumerator = x.GetEnumerator();
                var length = 0;
                var buffer = ArrayPool<char>.Shared.Rent(1024);
                fixed (char* c = buffer)
                {
                    Unicode.Utf8ToUtf16(x.GetUnsafePtr(), x.Length, c, out length, x.Length * 2);
                }
                target.SetText(buffer, 0, length);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString4096Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString4096Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                var enumerator = x.GetEnumerator();
                var length = 0;
                var buffer = ArrayPool<char>.Shared.Rent(8192);
                fixed (char* c = buffer)
                {
                    Unicode.Utf8ToUtf16(x.GetUnsafePtr(), x.Length, c, out length, x.Length * 2);
                }
                target.SetText(buffer, 0, length);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {

                var buffer = ArrayPool<char>.Shared.Rent(128);
                var bufferOffset = 0;
                Utf16StringHelper.WriteInt32(ref buffer, ref bufferOffset, x);
                target.SetText(buffer, 0, bufferOffset);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="format">Format string</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text, string format)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, format, (x, target, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                target.SetTextFormat(format, x);
#else
                target.text = string.Format(format, x);
#endif
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<long, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<long, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {

                var buffer = ArrayPool<char>.Shared.Rent(128);
                var bufferOffset = 0;
                Utf16StringHelper.WriteInt64(ref buffer, ref bufferOffset, x);
                target.SetText(buffer, 0, bufferOffset);
                ArrayPool<char>.Shared.Return(buffer);
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="format">Format string</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<long, TOptions, TAdapter> builder, TMP_Text text, string format)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<long, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, format, (x, target, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                target.SetTextFormat(format, x);
#else
                target.text = string.Format(format, x);
#endif
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <remarks>
        /// Note: This extension method uses TMP_Text.SetText() to achieve zero allocation, so it is recommended to use this method when binding to text.
        /// </remarks>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            const string format = "{0}";
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                target.SetTextFormat(format, x);
#else
                target.SetText(format, x);
#endif
            });
        }

        /// <summary>
        /// Create a motion data and bind it to TMP_Text.text.
        /// </summary>
        /// <typeparam name="TOptions">The type of special parameters given to the motion data</typeparam>
        /// <typeparam name="TAdapter">The type of adapter that support value animation</typeparam>
        /// <param name="builder">This builder</param>
        /// <param name="text">Target TMP_Text</param>
        /// <param name="format">Format string</param>
        /// <returns>Handle of the created motion data.</returns>
        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text, string format)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, format, (x, target, format) =>
            {
#if LITMOTION_SUPPORT_ZSTRING
                target.SetTextFormat(format, x);
#else
                target.text = string.Format(format, x);
#endif
            });
        }
    }
}
#endif