using System;
using UnityEngine;

namespace LitMotion.Animation
{
    public abstract class PropertyAnimationComponent<TObject, TValue, TOptions, TAdapter> : LitMotionAnimationComponent
        where TObject : UnityEngine.Object
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
    {
        [SerializeField] TObject target;
        [SerializeField] SerializableMotionSettings<TValue, TOptions> settings;
        [SerializeField] bool relative;

        readonly Action revertAction;
        TValue startValue;

        public PropertyAnimationComponent()
        {
            revertAction = Revert;
        }

        void Revert()
        {
            if (target == null) return;
            SetValue(target, startValue);
        }

        public override MotionHandle Play()
        {
            startValue = GetValue(target);

            if (relative)
            {
                return LMotion.Create<TValue, TOptions, TAdapter>(settings)
                    .WithOnCancel(revertAction)
                    .Bind(this, (x, state) =>
                    {
                        state.SetRelativeValue(target, state.startValue, x);
                    });
            }
            else
            {
                return LMotion.Create<TValue, TOptions, TAdapter>(settings)
                    .WithOnCancel(revertAction)
                    .Bind(this, (x, state) =>
                    {
                        state.SetValue(target, x);
                    });
            }
        }

        protected abstract TValue GetValue(TObject target);
        protected abstract void SetValue(TObject target, in TValue value);
        protected abstract void SetRelativeValue(TObject target, in TValue startValue, in TValue relativeValue);
    }
}