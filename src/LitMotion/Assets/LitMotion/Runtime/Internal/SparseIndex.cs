using System;

namespace LitMotion
{
    internal readonly struct SparseIndex : IEquatable<SparseIndex>
    {
        public int Index { get; }
        public int Version { get; }

        public SparseIndex(int index, int version)
        {
            Index = index;
            Version = version;
        }

        public override bool Equals(object obj)
        {
            return obj is SparseIndex other && Equals(other);
        }

        public bool Equals(SparseIndex other)
        {
            return Index == other.Index && Version == other.Version;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Version);
        }
    }
}