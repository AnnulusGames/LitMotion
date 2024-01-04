#if LITMOTION_SUPPORT_TMP
using System.Buffers;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Collections;
using TMPro;

namespace LitMotion.Extensions
{
    public static class LitMotionTextMeshProExtensions
    {
        public static MotionHandle BindToFontSize<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.fontSize = x;
            });
        }

        public static MotionHandle BindToMaxVisibleCharacters<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleCharacters = x;
            });
        }

        public static MotionHandle BindToMaxVisibleLines<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleLines = x;
            });
        }

        public static MotionHandle BindToMaxVisibleWords<TOptions, TAdapter>(this MotionBuilder<int, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<int, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleWords = x;
            });
        }

        public static MotionHandle BindToColor<TOptions, TAdapter>(this MotionBuilder<Color, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<Color, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.color = x;
            });
        }

        public static MotionHandle BindToColorR<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.r = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorG<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.g = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorB<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.b = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorA<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.a = x;
                target.color = c;
            });
        }

        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString32Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString32Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
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

        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString64Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString64Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
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

        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString128Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString128Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
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

        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString512Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString512Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
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

        public unsafe static MotionHandle BindToText<TOptions, TAdapter>(this MotionBuilder<FixedString4096Bytes, TOptions, TAdapter> builder, TMP_Text text)
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<FixedString4096Bytes, TOptions>
        {
            Error.IsNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
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
    }
}
#endif