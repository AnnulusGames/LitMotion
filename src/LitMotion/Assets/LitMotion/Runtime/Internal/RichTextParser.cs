using Unity.Burst;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace LitMotion
{
    [BurstCompile]
    internal static class RichTextParser
    {
        [BurstCompile]
        public static void GetSymbols(ref FixedString32Bytes source, Allocator allocator, out UnsafeList<RichTextSymbol32Bytes> symbols, out int charCount)
        {
            symbols = new UnsafeList<RichTextSymbol32Bytes>(32, allocator);
            charCount = 0;

            var buffer = new NativeText(32, Allocator.Temp);
            var enumerator = source.GetEnumerator();

            var currentSymbolType = RichTextSymbolType.Text;
            var prevRune = default(Unicode.Rune);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.value == '<' && currentSymbolType is not (RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd))
                {
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString32Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol32Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagStart;
                }
                else if (current.value == '/' && prevRune.value == '<')
                {
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagEnd;
                }
                else if (current.value == '>' && currentSymbolType is RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd)
                {
                    buffer.Append(current);
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString32Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol32Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    currentSymbolType = RichTextSymbolType.Text;
                }
                else
                {
                    buffer.Append(current);
                }

                prevRune = current;
            }

            if (buffer.Length > 0)
            {
                var text = new FixedString32Bytes();
                text.CopyFrom(buffer);
                symbols.Add(new RichTextSymbol32Bytes(currentSymbolType, text));
                charCount += FixedStringHelper.GetUtf8CharCount(ref text);
            }

            buffer.Dispose();
        }
        [BurstCompile]
        public static void GetSymbols(ref FixedString64Bytes source, Allocator allocator, out UnsafeList<RichTextSymbol64Bytes> symbols, out int charCount)
        {
            symbols = new UnsafeList<RichTextSymbol64Bytes>(32, allocator);
            charCount = 0;

            var buffer = new NativeText(64, Allocator.Temp);
            var enumerator = source.GetEnumerator();

            var currentSymbolType = RichTextSymbolType.Text;
            var prevRune = default(Unicode.Rune);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.value == '<' && currentSymbolType is not (RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd))
                {
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString64Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol64Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagStart;
                }
                else if (current.value == '/' && prevRune.value == '<')
                {
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagEnd;
                }
                else if (current.value == '>' && currentSymbolType is RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd)
                {
                    buffer.Append(current);
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString64Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol64Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    currentSymbolType = RichTextSymbolType.Text;
                }
                else
                {
                    buffer.Append(current);
                }

                prevRune = current;
            }

            if (buffer.Length > 0)
            {
                var text = new FixedString64Bytes();
                text.CopyFrom(buffer);
                symbols.Add(new RichTextSymbol64Bytes(currentSymbolType, text));
                charCount += FixedStringHelper.GetUtf8CharCount(ref text);
            }

            buffer.Dispose();
        }
        [BurstCompile]
        public static void GetSymbols(ref FixedString128Bytes source, Allocator allocator, out UnsafeList<RichTextSymbol128Bytes> symbols, out int charCount)
        {
            symbols = new UnsafeList<RichTextSymbol128Bytes>(32, allocator);
            charCount = 0;

            var buffer = new NativeText(128, Allocator.Temp);
            var enumerator = source.GetEnumerator();

            var currentSymbolType = RichTextSymbolType.Text;
            var prevRune = default(Unicode.Rune);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.value == '<' && currentSymbolType is not (RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd))
                {
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString128Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol128Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagStart;
                }
                else if (current.value == '/' && prevRune.value == '<')
                {
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagEnd;
                }
                else if (current.value == '>' && currentSymbolType is RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd)
                {
                    buffer.Append(current);
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString128Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol128Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    currentSymbolType = RichTextSymbolType.Text;
                }
                else
                {
                    buffer.Append(current);
                }

                prevRune = current;
            }

            if (buffer.Length > 0)
            {
                var text = new FixedString128Bytes();
                text.CopyFrom(buffer);
                symbols.Add(new RichTextSymbol128Bytes(currentSymbolType, text));
                charCount += FixedStringHelper.GetUtf8CharCount(ref text);
            }

            buffer.Dispose();
        }
        [BurstCompile]
        public static void GetSymbols(ref FixedString512Bytes source, Allocator allocator, out UnsafeList<RichTextSymbol512Bytes> symbols, out int charCount)
        {
            symbols = new UnsafeList<RichTextSymbol512Bytes>(32, allocator);
            charCount = 0;

            var buffer = new NativeText(512, Allocator.Temp);
            var enumerator = source.GetEnumerator();

            var currentSymbolType = RichTextSymbolType.Text;
            var prevRune = default(Unicode.Rune);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.value == '<' && currentSymbolType is not (RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd))
                {
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString512Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol512Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagStart;
                }
                else if (current.value == '/' && prevRune.value == '<')
                {
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagEnd;
                }
                else if (current.value == '>' && currentSymbolType is RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd)
                {
                    buffer.Append(current);
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString512Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol512Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    currentSymbolType = RichTextSymbolType.Text;
                }
                else
                {
                    buffer.Append(current);
                }

                prevRune = current;
            }

            if (buffer.Length > 0)
            {
                var text = new FixedString512Bytes();
                text.CopyFrom(buffer);
                symbols.Add(new RichTextSymbol512Bytes(currentSymbolType, text));
                charCount += FixedStringHelper.GetUtf8CharCount(ref text);
            }

            buffer.Dispose();
        }
        [BurstCompile]
        public static void GetSymbols(ref FixedString4096Bytes source, Allocator allocator, out UnsafeList<RichTextSymbol4096Bytes> symbols, out int charCount)
        {
            symbols = new UnsafeList<RichTextSymbol4096Bytes>(32, allocator);
            charCount = 0;

            var buffer = new NativeText(4096, Allocator.Temp);
            var enumerator = source.GetEnumerator();

            var currentSymbolType = RichTextSymbolType.Text;
            var prevRune = default(Unicode.Rune);

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;

                if (current.value == '<' && currentSymbolType is not (RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd))
                {
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString4096Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol4096Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagStart;
                }
                else if (current.value == '/' && prevRune.value == '<')
                {
                    buffer.Append(current);
                    currentSymbolType = RichTextSymbolType.TagEnd;
                }
                else if (current.value == '>' && currentSymbolType is RichTextSymbolType.TagStart or RichTextSymbolType.TagEnd)
                {
                    buffer.Append(current);
                    if (buffer.Length > 0)
                    {
                        var text = new FixedString4096Bytes();
                        text.CopyFrom(buffer);
                        symbols.Add(new RichTextSymbol4096Bytes(currentSymbolType, text));
                        if (currentSymbolType == RichTextSymbolType.Text) charCount += FixedStringHelper.GetUtf8CharCount(ref text);
                        buffer.Clear();
                    }
                    currentSymbolType = RichTextSymbolType.Text;
                }
                else
                {
                    buffer.Append(current);
                }

                prevRune = current;
            }

            if (buffer.Length > 0)
            {
                var text = new FixedString4096Bytes();
                text.CopyFrom(buffer);
                symbols.Add(new RichTextSymbol4096Bytes(currentSymbolType, text));
                charCount += FixedStringHelper.GetUtf8CharCount(ref text);
            }

            buffer.Dispose();
        }
    }
}