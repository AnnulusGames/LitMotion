using Unity.Burst;
using Unity.Mathematics;

namespace LitMotion
{
    static class SharedRandom
    {
        readonly struct Key { }
        static readonly SharedStatic<Random> sharedStatic = SharedStatic<Random>.GetOrCreate<Key>();

        public static ref Random Random
        {
            get
            {
                if (sharedStatic.Data.state == 0) sharedStatic.Data.InitState();
                return ref sharedStatic.Data;
            }
        }
    }
}