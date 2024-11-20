using System;

namespace LitMotion
{
    internal readonly struct Entity : IEquatable<Entity>
    {
        public int Index { get; }
        public int Version { get; }

        public Entity(int index, int version)
        {
            Index = index;
            Version = version;
        }

        public override bool Equals(object obj)
        {
            return obj is Entity other && Equals(other);
        }

        public bool Equals(Entity other)
        {
            return Index == other.Index && Version == other.Version;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Index, Version);
        }
    }
}