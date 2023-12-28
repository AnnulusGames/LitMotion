#if LITMOTION_SUPPORT_TMP
using System.Buffers;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Collections;
using TMPro;
using LitMotion.Adapters;

namespace LitMotion.Extensions
{
    public static class LitMotionTextMeshProExtensions
    {
        public static MotionHandle BindToFontSize(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.fontSize = x;
            });
        }

        public static MotionHandle BindToMaxVisibleCharacters(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleCharacters = x;
            });
        }

        public static MotionHandle BindToMaxVisibleLines(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleLines = x;
            });
        }

        public static MotionHandle BindToMaxVisibleWords(this MotionBuilder<int, IntegerOptions, IntMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.maxVisibleWords = x;
            });
        }

        public static MotionHandle BindToColor(this MotionBuilder<Color, NoOptions, ColorMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                target.color = x;
            });
        }

        public static MotionHandle BindToColorR(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.r = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorG(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.g = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorB(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.b = x;
                target.color = c;
            });
        }

        public static MotionHandle BindToColorA(this MotionBuilder<float, NoOptions, FloatMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
            return builder.BindWithState(text, (x, target) =>
            {
                if (target == null) return;
                var c = target.color;
                c.a = x;
                target.color = c;
            });
        }

        public unsafe static MotionHandle BindToText(this MotionBuilder<FixedString32Bytes, StringOptions, FixedString32BytesMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
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

        public unsafe static MotionHandle BindToText(this MotionBuilder<FixedString64Bytes, StringOptions, FixedString64BytesMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
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

        public unsafe static MotionHandle BindToText(this MotionBuilder<FixedString128Bytes, StringOptions, FixedString128BytesMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
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

        public unsafe static MotionHandle BindToText(this MotionBuilder<FixedString512Bytes, StringOptions, FixedString512BytesMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
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

        public unsafe static MotionHandle BindToText(this MotionBuilder<FixedString4096Bytes, StringOptions, FixedString4096BytesMotionAdapter> builder, TMP_Text text)
        {
            Assert.IsNotNull(text);
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