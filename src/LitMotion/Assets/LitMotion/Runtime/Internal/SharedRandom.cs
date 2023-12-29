using Unity.Burst;
using Unity.Mathematics;

namespace LitMotion
{
    static class SharedRandom
    {
        readonly struct Key { }
        public static readonly SharedStatic<Random> Random;

        static SharedRandom()
        {
            Random = SharedStatic<Random>.GetOrCreate<Key>();
            Random.Data.InitState();
        }
    }
}