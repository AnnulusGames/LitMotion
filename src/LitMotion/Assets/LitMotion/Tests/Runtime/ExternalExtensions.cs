namespace LitMotion.Tests.Runtime
{
    static class ExternalExtensions
    {
#if LITMOTION_TEST_R3
        public static R3.Observable<TValue> ToR3Observable<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return LitMotionR3Extensions.ToObservable(builder);
        }
#endif

#if LITMOTION_TEST_UNIRX
        public static System.IObservable<TValue> ToRxObservable<TValue, TOptions, TAdapter>(this MotionBuilder<TValue, TOptions, TAdapter> builder)
            where TValue : unmanaged
            where TOptions : unmanaged, IMotionOptions
            where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        {
            return LitMotionUniRxExtensions.ToObservable(builder);
        }
#endif
    }
}