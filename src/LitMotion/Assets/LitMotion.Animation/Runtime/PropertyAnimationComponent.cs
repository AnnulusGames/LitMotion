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

        protected void Revert()
        {
            if (target == null) return;
            SetValue(target, startValue);
            OnRevert(target);
        }

        protected virtual void OnBeforePlay(TObject target) { }
        protected virtual void OnAfterPlay(TObject target) { }
        protected virtual void OnRevert(TObject target) { }

        public override MotionHandle Play()
        {
            startValue = GetValue(target);

            OnBeforePlay(target);

            MotionHandle handle;

            if (relative)
            {
                handle = LMotion.Create<TValue, TOptions, TAdapter>(settings)
                    .WithOnCancel(revertAction)
                    .Bind(this, (x, state) =>
                    {
                        state.SetRelativeValue(target, state.startValue, x);
                    });
            }
            else
            {
                handle = LMotion.Create<TValue, TOptions, TAdapter>(settings)
                    .WithOnCancel(revertAction)
                    .Bind(this, (x, state) =>
                    {
                        state.SetValue(target, x);
                    });
            }

            OnAfterPlay(target);

            return handle;
        }

        protected abstract TValue GetValue(TObject target);
        protected abstract void SetValue(TObject target, in TValue value);
        protected abstract void SetRelativeValue(TObject target, in TValue startValue, in TValue relativeValue);
    }
}