using Unity.Collections;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections.LowLevel.Unsafe;

namespace LitMotion
{
    internal static partial class FixedStringHelper
    {
        [BurstCompile]
        public static int GetUtf8CharCount(ref FixedString32Bytes runes)
        {
            int length = 0;
            var enumerator = runes.GetEnumerator();
            while (enumerator.MoveNext()) length++;
            return length;
        }

        static Unicode.Rune GetRuneOf(ref FixedString32Bytes text, int charIndex)
        {
            int index = 0;
            var enumerator = text.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (index == charIndex) return enumerator.Current;
                index++;
            }
            return Unicode.BadRune;
        }

        [BurstCompile]
        public static void Interpolate(ref FixedString32Bytes start, ref FixedString32Bytes end, float t, ScrambleMode scrambleMode, bool richTextEnabled, ref Random randomState, ref FixedString64Bytes customScrambleChars, out FixedString32Bytes result)
        {
            if (richTextEnabled)
            {
                RichTextParser.GetSymbols(ref start, Allocator.Temp, out var startTextSymbols, out var startTextUtf8Length);
                RichTextParser.GetSymbols(ref end, Allocator.Temp, out var endTextSymbols, out var endTextUtf8Length);

                FillRichText(ref startTextSymbols, ref endTextSymbols, startTextUtf8Length, endTextUtf8Length, t, scrambleMode, ref randomState, ref customScrambleChars, out result);

                startTextSymbols.Dispose();
                endTextSymbols.Dispose();
            }
            else
            {
                FillText(ref start, ref end, t, scrambleMode, ref randomState, ref customScrambleChars, out result);
            }
        }

        unsafe static void FillText(
            ref FixedString32Bytes start,
            ref FixedString32Bytes end,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString32Bytes result)
        {
            var startTextUtf8Length = GetUtf8CharCount(ref start);
            var endTextUtf8Length = GetUtf8CharCount(ref end);
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var enumeratorStart = start.GetEnumerator();
            var enumeratorEnd = end.GetEnumerator();
            result = new();
            
            for (int i = 0; i < length; i++)
            {
                var startMoveNext = enumeratorStart.MoveNext();
                var endMoveNext = enumeratorEnd.MoveNext();

                if (i < currentTextLength)
                {
                    if (endMoveNext)
                    {
                        result.Append(enumeratorEnd.Current);
                    }
                }
                else
                {
                    if (startMoveNext)
                    {
                        result.Append(enumeratorStart.Current);
                    }
                }
            }

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - currentTextLength);
        }

        unsafe static void FillRichText(
            ref UnsafeList<RichTextSymbol32Bytes> startSymbols,
            ref UnsafeList<RichTextSymbol32Bytes> endSymbols,
            int startTextUtf8Length,
            int endTextUtf8Length,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString32Bytes result)
        {
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var slicedText1 = SliceSymbols(ref endSymbols, 0, currentTextLength, out var length1);
            var slicedText2 = SliceSymbols(ref startSymbols, currentTextLength + 1, length - 1, out var length2);

            result = new FixedString32Bytes();
            result.Append(slicedText1);
            result.Append(slicedText2);

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - (length1 + length2));
        }

        unsafe static void FillScrambleChars(
            ref FixedString32Bytes target,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            int count)
        {
            if (scrambleMode == ScrambleMode.None) return;
            if (randomState.state == 0) randomState.InitState();

            if (scrambleMode == ScrambleMode.Custom)
            {
                var customScrambleCharsUtf8Length = GetUtf8CharCount(ref customScrambleChars);
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetRuneOf(ref customScrambleChars, randomState.NextInt(0, customScrambleCharsUtf8Length)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetScrambleChar(scrambleMode, ref randomState));
                }
            }
        }

        unsafe static FixedString32Bytes SliceSymbols(ref UnsafeList<RichTextSymbol32Bytes> symbols, int from, int to, out int resultRichTextLength)
        {
            var text = new FixedString32Bytes();
            RichTextSymbol32Bytes* symbolsPtr = symbols.Ptr;
            var offset = 0;
            var tagIndent = 0;
            resultRichTextLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                RichTextSymbol32Bytes* symbol = symbolsPtr + i;
                switch (symbol->Type)
                {
                    case RichTextSymbolType.Text:
                        var enumerator = symbol->Text.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (from <= offset && offset < to)
                            {
                                text.Append(current);
                                resultRichTextLength++;
                            }
                            offset++;

                            if (offset >= to && tagIndent == 0) goto LOOP_END;
                        }
                        break;
                    case RichTextSymbolType.TagStart:
                        text.Append(symbol->Text);
                        tagIndent++;
                        break;
                    case RichTextSymbolType.TagEnd:
                        text.Append(symbol->Text);
                        tagIndent--;
                        if (offset >= to && tagIndent == 0) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            return text;
        }

        [BurstCompile]
        public static int GetUtf8CharCount(ref FixedString64Bytes runes)
        {
            int length = 0;
            var enumerator = runes.GetEnumerator();
            while (enumerator.MoveNext()) length++;
            return length;
        }

        static Unicode.Rune GetRuneOf(ref FixedString64Bytes text, int charIndex)
        {
            int index = 0;
            var enumerator = text.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (index == charIndex) return enumerator.Current;
                index++;
            }
            return Unicode.BadRune;
        }

        [BurstCompile]
        public static void Interpolate(ref FixedString64Bytes start, ref FixedString64Bytes end, float t, ScrambleMode scrambleMode, bool richTextEnabled, ref Random randomState, ref FixedString64Bytes customScrambleChars, out FixedString64Bytes result)
        {
            if (richTextEnabled)
            {
                RichTextParser.GetSymbols(ref start, Allocator.Temp, out var startTextSymbols, out var startTextUtf8Length);
                RichTextParser.GetSymbols(ref end, Allocator.Temp, out var endTextSymbols, out var endTextUtf8Length);

                FillRichText(ref startTextSymbols, ref endTextSymbols, startTextUtf8Length, endTextUtf8Length, t, scrambleMode, ref randomState, ref customScrambleChars, out result);

                startTextSymbols.Dispose();
                endTextSymbols.Dispose();
            }
            else
            {
                FillText(ref start, ref end, t, scrambleMode, ref randomState, ref customScrambleChars, out result);
            }
        }

        unsafe static void FillText(
            ref FixedString64Bytes start,
            ref FixedString64Bytes end,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString64Bytes result)
        {
            var startTextUtf8Length = GetUtf8CharCount(ref start);
            var endTextUtf8Length = GetUtf8CharCount(ref end);
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var enumeratorStart = start.GetEnumerator();
            var enumeratorEnd = end.GetEnumerator();
            result = new();
            
            for (int i = 0; i < length; i++)
            {
                var startMoveNext = enumeratorStart.MoveNext();
                var endMoveNext = enumeratorEnd.MoveNext();

                if (i < currentTextLength)
                {
                    if (endMoveNext)
                    {
                        result.Append(enumeratorEnd.Current);
                    }
                }
                else
                {
                    if (startMoveNext)
                    {
                        result.Append(enumeratorStart.Current);
                    }
                }
            }

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - currentTextLength);
        }

        unsafe static void FillRichText(
            ref UnsafeList<RichTextSymbol64Bytes> startSymbols,
            ref UnsafeList<RichTextSymbol64Bytes> endSymbols,
            int startTextUtf8Length,
            int endTextUtf8Length,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString64Bytes result)
        {
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var slicedText1 = SliceSymbols(ref endSymbols, 0, currentTextLength, out var length1);
            var slicedText2 = SliceSymbols(ref startSymbols, currentTextLength + 1, length - 1, out var length2);

            result = new FixedString64Bytes();
            result.Append(slicedText1);
            result.Append(slicedText2);

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - (length1 + length2));
        }

        unsafe static void FillScrambleChars(
            ref FixedString64Bytes target,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            int count)
        {
            if (scrambleMode == ScrambleMode.None) return;
            if (randomState.state == 0) randomState.InitState();

            if (scrambleMode == ScrambleMode.Custom)
            {
                var customScrambleCharsUtf8Length = GetUtf8CharCount(ref customScrambleChars);
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetRuneOf(ref customScrambleChars, randomState.NextInt(0, customScrambleCharsUtf8Length)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetScrambleChar(scrambleMode, ref randomState));
                }
            }
        }

        unsafe static FixedString64Bytes SliceSymbols(ref UnsafeList<RichTextSymbol64Bytes> symbols, int from, int to, out int resultRichTextLength)
        {
            var text = new FixedString64Bytes();
            RichTextSymbol64Bytes* symbolsPtr = symbols.Ptr;
            var offset = 0;
            var tagIndent = 0;
            resultRichTextLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                RichTextSymbol64Bytes* symbol = symbolsPtr + i;
                switch (symbol->Type)
                {
                    case RichTextSymbolType.Text:
                        var enumerator = symbol->Text.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (from <= offset && offset < to)
                            {
                                text.Append(current);
                                resultRichTextLength++;
                            }
                            offset++;

                            if (offset >= to && tagIndent == 0) goto LOOP_END;
                        }
                        break;
                    case RichTextSymbolType.TagStart:
                        text.Append(symbol->Text);
                        tagIndent++;
                        break;
                    case RichTextSymbolType.TagEnd:
                        text.Append(symbol->Text);
                        tagIndent--;
                        if (offset >= to && tagIndent == 0) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            return text;
        }

        [BurstCompile]
        public static int GetUtf8CharCount(ref FixedString128Bytes runes)
        {
            int length = 0;
            var enumerator = runes.GetEnumerator();
            while (enumerator.MoveNext()) length++;
            return length;
        }

        static Unicode.Rune GetRuneOf(ref FixedString128Bytes text, int charIndex)
        {
            int index = 0;
            var enumerator = text.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (index == charIndex) return enumerator.Current;
                index++;
            }
            return Unicode.BadRune;
        }

        [BurstCompile]
        public static void Interpolate(ref FixedString128Bytes start, ref FixedString128Bytes end, float t, ScrambleMode scrambleMode, bool richTextEnabled, ref Random randomState, ref FixedString64Bytes customScrambleChars, out FixedString128Bytes result)
        {
            if (richTextEnabled)
            {
                RichTextParser.GetSymbols(ref start, Allocator.Temp, out var startTextSymbols, out var startTextUtf8Length);
                RichTextParser.GetSymbols(ref end, Allocator.Temp, out var endTextSymbols, out var endTextUtf8Length);

                FillRichText(ref startTextSymbols, ref endTextSymbols, startTextUtf8Length, endTextUtf8Length, t, scrambleMode, ref randomState, ref customScrambleChars, out result);

                startTextSymbols.Dispose();
                endTextSymbols.Dispose();
            }
            else
            {
                FillText(ref start, ref end, t, scrambleMode, ref randomState, ref customScrambleChars, out result);
            }
        }

        unsafe static void FillText(
            ref FixedString128Bytes start,
            ref FixedString128Bytes end,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString128Bytes result)
        {
            var startTextUtf8Length = GetUtf8CharCount(ref start);
            var endTextUtf8Length = GetUtf8CharCount(ref end);
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var enumeratorStart = start.GetEnumerator();
            var enumeratorEnd = end.GetEnumerator();
            result = new();
            
            for (int i = 0; i < length; i++)
            {
                var startMoveNext = enumeratorStart.MoveNext();
                var endMoveNext = enumeratorEnd.MoveNext();

                if (i < currentTextLength)
                {
                    if (endMoveNext)
                    {
                        result.Append(enumeratorEnd.Current);
                    }
                }
                else
                {
                    if (startMoveNext)
                    {
                        result.Append(enumeratorStart.Current);
                    }
                }
            }

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - currentTextLength);
        }

        unsafe static void FillRichText(
            ref UnsafeList<RichTextSymbol128Bytes> startSymbols,
            ref UnsafeList<RichTextSymbol128Bytes> endSymbols,
            int startTextUtf8Length,
            int endTextUtf8Length,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString128Bytes result)
        {
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var slicedText1 = SliceSymbols(ref endSymbols, 0, currentTextLength, out var length1);
            var slicedText2 = SliceSymbols(ref startSymbols, currentTextLength + 1, length - 1, out var length2);

            result = new FixedString128Bytes();
            result.Append(slicedText1);
            result.Append(slicedText2);

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - (length1 + length2));
        }

        unsafe static void FillScrambleChars(
            ref FixedString128Bytes target,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            int count)
        {
            if (scrambleMode == ScrambleMode.None) return;
            if (randomState.state == 0) randomState.InitState();

            if (scrambleMode == ScrambleMode.Custom)
            {
                var customScrambleCharsUtf8Length = GetUtf8CharCount(ref customScrambleChars);
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetRuneOf(ref customScrambleChars, randomState.NextInt(0, customScrambleCharsUtf8Length)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetScrambleChar(scrambleMode, ref randomState));
                }
            }
        }

        unsafe static FixedString128Bytes SliceSymbols(ref UnsafeList<RichTextSymbol128Bytes> symbols, int from, int to, out int resultRichTextLength)
        {
            var text = new FixedString128Bytes();
            RichTextSymbol128Bytes* symbolsPtr = symbols.Ptr;
            var offset = 0;
            var tagIndent = 0;
            resultRichTextLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                RichTextSymbol128Bytes* symbol = symbolsPtr + i;
                switch (symbol->Type)
                {
                    case RichTextSymbolType.Text:
                        var enumerator = symbol->Text.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (from <= offset && offset < to)
                            {
                                text.Append(current);
                                resultRichTextLength++;
                            }
                            offset++;

                            if (offset >= to && tagIndent == 0) goto LOOP_END;
                        }
                        break;
                    case RichTextSymbolType.TagStart:
                        text.Append(symbol->Text);
                        tagIndent++;
                        break;
                    case RichTextSymbolType.TagEnd:
                        text.Append(symbol->Text);
                        tagIndent--;
                        if (offset >= to && tagIndent == 0) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            return text;
        }

        [BurstCompile]
        public static int GetUtf8CharCount(ref FixedString512Bytes runes)
        {
            int length = 0;
            var enumerator = runes.GetEnumerator();
            while (enumerator.MoveNext()) length++;
            return length;
        }

        static Unicode.Rune GetRuneOf(ref FixedString512Bytes text, int charIndex)
        {
            int index = 0;
            var enumerator = text.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (index == charIndex) return enumerator.Current;
                index++;
            }
            return Unicode.BadRune;
        }

        [BurstCompile]
        public static void Interpolate(ref FixedString512Bytes start, ref FixedString512Bytes end, float t, ScrambleMode scrambleMode, bool richTextEnabled, ref Random randomState, ref FixedString64Bytes customScrambleChars, out FixedString512Bytes result)
        {
            if (richTextEnabled)
            {
                RichTextParser.GetSymbols(ref start, Allocator.Temp, out var startTextSymbols, out var startTextUtf8Length);
                RichTextParser.GetSymbols(ref end, Allocator.Temp, out var endTextSymbols, out var endTextUtf8Length);

                FillRichText(ref startTextSymbols, ref endTextSymbols, startTextUtf8Length, endTextUtf8Length, t, scrambleMode, ref randomState, ref customScrambleChars, out result);

                startTextSymbols.Dispose();
                endTextSymbols.Dispose();
            }
            else
            {
                FillText(ref start, ref end, t, scrambleMode, ref randomState, ref customScrambleChars, out result);
            }
        }

        unsafe static void FillText(
            ref FixedString512Bytes start,
            ref FixedString512Bytes end,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString512Bytes result)
        {
            var startTextUtf8Length = GetUtf8CharCount(ref start);
            var endTextUtf8Length = GetUtf8CharCount(ref end);
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var enumeratorStart = start.GetEnumerator();
            var enumeratorEnd = end.GetEnumerator();
            result = new();
            
            for (int i = 0; i < length; i++)
            {
                var startMoveNext = enumeratorStart.MoveNext();
                var endMoveNext = enumeratorEnd.MoveNext();

                if (i < currentTextLength)
                {
                    if (endMoveNext)
                    {
                        result.Append(enumeratorEnd.Current);
                    }
                }
                else
                {
                    if (startMoveNext)
                    {
                        result.Append(enumeratorStart.Current);
                    }
                }
            }

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - currentTextLength);
        }

        unsafe static void FillRichText(
            ref UnsafeList<RichTextSymbol512Bytes> startSymbols,
            ref UnsafeList<RichTextSymbol512Bytes> endSymbols,
            int startTextUtf8Length,
            int endTextUtf8Length,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString512Bytes result)
        {
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var slicedText1 = SliceSymbols(ref endSymbols, 0, currentTextLength, out var length1);
            var slicedText2 = SliceSymbols(ref startSymbols, currentTextLength + 1, length - 1, out var length2);

            result = new FixedString512Bytes();
            result.Append(slicedText1);
            result.Append(slicedText2);

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - (length1 + length2));
        }

        unsafe static void FillScrambleChars(
            ref FixedString512Bytes target,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            int count)
        {
            if (scrambleMode == ScrambleMode.None) return;
            if (randomState.state == 0) randomState.InitState();

            if (scrambleMode == ScrambleMode.Custom)
            {
                var customScrambleCharsUtf8Length = GetUtf8CharCount(ref customScrambleChars);
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetRuneOf(ref customScrambleChars, randomState.NextInt(0, customScrambleCharsUtf8Length)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetScrambleChar(scrambleMode, ref randomState));
                }
            }
        }

        unsafe static FixedString512Bytes SliceSymbols(ref UnsafeList<RichTextSymbol512Bytes> symbols, int from, int to, out int resultRichTextLength)
        {
            var text = new FixedString512Bytes();
            RichTextSymbol512Bytes* symbolsPtr = symbols.Ptr;
            var offset = 0;
            var tagIndent = 0;
            resultRichTextLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                RichTextSymbol512Bytes* symbol = symbolsPtr + i;
                switch (symbol->Type)
                {
                    case RichTextSymbolType.Text:
                        var enumerator = symbol->Text.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (from <= offset && offset < to)
                            {
                                text.Append(current);
                                resultRichTextLength++;
                            }
                            offset++;

                            if (offset >= to && tagIndent == 0) goto LOOP_END;
                        }
                        break;
                    case RichTextSymbolType.TagStart:
                        text.Append(symbol->Text);
                        tagIndent++;
                        break;
                    case RichTextSymbolType.TagEnd:
                        text.Append(symbol->Text);
                        tagIndent--;
                        if (offset >= to && tagIndent == 0) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            return text;
        }

        [BurstCompile]
        public static int GetUtf8CharCount(ref FixedString4096Bytes runes)
        {
            int length = 0;
            var enumerator = runes.GetEnumerator();
            while (enumerator.MoveNext()) length++;
            return length;
        }

        static Unicode.Rune GetRuneOf(ref FixedString4096Bytes text, int charIndex)
        {
            int index = 0;
            var enumerator = text.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (index == charIndex) return enumerator.Current;
                index++;
            }
            return Unicode.BadRune;
        }

        [BurstCompile]
        public static void Interpolate(ref FixedString4096Bytes start, ref FixedString4096Bytes end, float t, ScrambleMode scrambleMode, bool richTextEnabled, ref Random randomState, ref FixedString64Bytes customScrambleChars, out FixedString4096Bytes result)
        {
            if (richTextEnabled)
            {
                RichTextParser.GetSymbols(ref start, Allocator.Temp, out var startTextSymbols, out var startTextUtf8Length);
                RichTextParser.GetSymbols(ref end, Allocator.Temp, out var endTextSymbols, out var endTextUtf8Length);

                FillRichText(ref startTextSymbols, ref endTextSymbols, startTextUtf8Length, endTextUtf8Length, t, scrambleMode, ref randomState, ref customScrambleChars, out result);

                startTextSymbols.Dispose();
                endTextSymbols.Dispose();
            }
            else
            {
                FillText(ref start, ref end, t, scrambleMode, ref randomState, ref customScrambleChars, out result);
            }
        }

        unsafe static void FillText(
            ref FixedString4096Bytes start,
            ref FixedString4096Bytes end,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString4096Bytes result)
        {
            var startTextUtf8Length = GetUtf8CharCount(ref start);
            var endTextUtf8Length = GetUtf8CharCount(ref end);
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var enumeratorStart = start.GetEnumerator();
            var enumeratorEnd = end.GetEnumerator();
            result = new();
            
            for (int i = 0; i < length; i++)
            {
                var startMoveNext = enumeratorStart.MoveNext();
                var endMoveNext = enumeratorEnd.MoveNext();

                if (i < currentTextLength)
                {
                    if (endMoveNext)
                    {
                        result.Append(enumeratorEnd.Current);
                    }
                }
                else
                {
                    if (startMoveNext)
                    {
                        result.Append(enumeratorStart.Current);
                    }
                }
            }

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - currentTextLength);
        }

        unsafe static void FillRichText(
            ref UnsafeList<RichTextSymbol4096Bytes> startSymbols,
            ref UnsafeList<RichTextSymbol4096Bytes> endSymbols,
            int startTextUtf8Length,
            int endTextUtf8Length,
            float t,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            out FixedString4096Bytes result)
        {
            var length = math.max(startTextUtf8Length, endTextUtf8Length);
            var currentTextLength = (int)math.round(length * t);

            var slicedText1 = SliceSymbols(ref endSymbols, 0, currentTextLength, out var length1);
            var slicedText2 = SliceSymbols(ref startSymbols, currentTextLength + 1, length - 1, out var length2);

            result = new FixedString4096Bytes();
            result.Append(slicedText1);
            result.Append(slicedText2);

            FillScrambleChars(ref result, scrambleMode, ref randomState, ref customScrambleChars, length - (length1 + length2));
        }

        unsafe static void FillScrambleChars(
            ref FixedString4096Bytes target,
            ScrambleMode scrambleMode,
            ref Random randomState,
            ref FixedString64Bytes customScrambleChars,
            int count)
        {
            if (scrambleMode == ScrambleMode.None) return;
            if (randomState.state == 0) randomState.InitState();

            if (scrambleMode == ScrambleMode.Custom)
            {
                var customScrambleCharsUtf8Length = GetUtf8CharCount(ref customScrambleChars);
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetRuneOf(ref customScrambleChars, randomState.NextInt(0, customScrambleCharsUtf8Length)));
                }
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    target.Append(GetScrambleChar(scrambleMode, ref randomState));
                }
            }
        }

        unsafe static FixedString4096Bytes SliceSymbols(ref UnsafeList<RichTextSymbol4096Bytes> symbols, int from, int to, out int resultRichTextLength)
        {
            var text = new FixedString4096Bytes();
            RichTextSymbol4096Bytes* symbolsPtr = symbols.Ptr;
            var offset = 0;
            var tagIndent = 0;
            resultRichTextLength = 0;

            for (int i = 0; i < symbols.Length; i++)
            {
                RichTextSymbol4096Bytes* symbol = symbolsPtr + i;
                switch (symbol->Type)
                {
                    case RichTextSymbolType.Text:
                        var enumerator = symbol->Text.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            var current = enumerator.Current;
                            if (from <= offset && offset < to)
                            {
                                text.Append(current);
                                resultRichTextLength++;
                            }
                            offset++;

                            if (offset >= to && tagIndent == 0) goto LOOP_END;
                        }
                        break;
                    case RichTextSymbolType.TagStart:
                        text.Append(symbol->Text);
                        tagIndent++;
                        break;
                    case RichTextSymbolType.TagEnd:
                        text.Append(symbol->Text);
                        tagIndent--;
                        if (offset >= to && tagIndent == 0) goto LOOP_END;
                        break;
                }
            }

        LOOP_END:
            return text;
        }

    }
}