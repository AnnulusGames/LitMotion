namespace LitMotion.Sequences.Components
{
    public abstract class PropertyComponentBase<TValue, TOptions, TAdapter, TObject> : SequenceComponentBase<TValue, TObject>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        where TObject : UnityEngine.Object
    {
        public override void Configure(ISequencePropertyTable sequencePropertyTable, SequenceItemBuilder builder)
        {
            var target = ResolveTarget(sequencePropertyTable);
            if (target == null) return;

            var type = GetType();
            if (!sequencePropertyTable.TryGetInitialValue((target, type), out TValue initialValue))
            {
                initialValue = GetValue(target);
                sequencePropertyTable.SetInitialValue((target, type), initialValue);
            }

            TValue currentValue = default;
            switch (MotionMode)
            {
                case MotionMode.Relative:
                    currentValue = initialValue;
                    break;
                case MotionMode.Additive:
                    currentValue = GetValue(target);
                    break;
            }

            var motionBuilder = LMotion.Create<TValue, TOptions, TAdapter>(GetRelativeValue(currentValue, StartValue), GetRelativeValue(currentValue, EndValue), Duration);
            ConfigureMotionBuilder(ref motionBuilder);

            var handle = motionBuilder.BindWithState(target, (x, target) => SetValue(target, x));
            builder.Add(handle);
        }

        public override void RestoreValues(ISequencePropertyTable sequencePropertyTable)
        {
            var target = ResolveTarget(sequencePropertyTable);
            if (target == null) return;

            if (sequencePropertyTable.TryGetInitialValue((target, GetType()), out TValue initialValue))
            {
                SetValue(target, initialValue);
            }
        }

        protected abstract TValue GetValue(TObject obj);
        protected abstract void SetValue(TObject obj, TValue value);
        protected abstract TValue GetRelativeValue(TValue start, TValue end);
    }
}
