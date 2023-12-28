using Unity.Collections;

namespace LitMotion
{
    internal enum RichTextSymbolType : byte
    {
        Text,
        TagStart,
        TagEnd
    }

    internal readonly struct RichTextSymbol32Bytes
    {
        public RichTextSymbol32Bytes(RichTextSymbolType type, in FixedString32Bytes text)
        {
            Type = type;
            Text = text;
        }

        public readonly RichTextSymbolType Type;
        public readonly FixedString32Bytes Text;
    }

    internal readonly struct RichTextSymbol64Bytes
    {
        public RichTextSymbol64Bytes(RichTextSymbolType type, in FixedString64Bytes text)
        {
            Type = type;
            Text = text;
        }

        public readonly RichTextSymbolType Type;
        public readonly FixedString64Bytes Text;
    }

    internal readonly struct RichTextSymbol128Bytes
    {
        public RichTextSymbol128Bytes(RichTextSymbolType type, in FixedString128Bytes text)
        {
            Type = type;
            Text = text;
        }

        public readonly RichTextSymbolType Type;
        public readonly FixedString128Bytes Text;
    }

    internal readonly struct RichTextSymbol512Bytes
    {
        public RichTextSymbol512Bytes(RichTextSymbolType type, in FixedString512Bytes text)
        {
            Type = type;
            Text = text;
        }

        public readonly RichTextSymbolType Type;
        public readonly FixedString512Bytes Text;
    }

    internal struct RichTextSymbol4096Bytes
    {
        public RichTextSymbol4096Bytes(RichTextSymbolType type, in FixedString4096Bytes text)
        {
            Type = type;
            Text = text;
        }

        public RichTextSymbolType Type;
        public FixedString4096Bytes Text;
    }
}