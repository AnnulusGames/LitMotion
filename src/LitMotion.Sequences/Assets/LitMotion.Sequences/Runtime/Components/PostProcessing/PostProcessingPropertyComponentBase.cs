#if LITMOTION_SUPPORT_RP
using System;
using UnityEngine.Rendering;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion.Sequences.Components
{
    public abstract class PostProcessingPropertyComponentBase<TValue, TOptions, TAdapter, TVolumeComponent> : PropertyComponentBase<TValue, TOptions, TAdapter, Volume>
        where TValue : unmanaged
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<TValue, TOptions>
        where TVolumeComponent : VolumeComponent
    {
        public override void Configure(ISequencePropertyTable propertyTable, SequenceItemBuilder builder)
        {
            var volume = ResolveTarget(propertyTable);
            if (volume == null) return;

            var target = volume.profile;
#if UNITY_EDITOR
            target.name += " (Instance)";
#endif
            if (!target.TryGet<TVolumeComponent>(out var vComponent)) return;

            TempData.defaultProfile = volume.sharedProfile;
            TempData.modifiedProfile = target;
            TempData.profile = volume;

            volume.sharedProfile = target;

            var type = GetType();
            if (!propertyTable.TryGetInitialValue((vComponent, type), out TValue initialValue))
            {
                initialValue = GetValue(vComponent);
                propertyTable.SetInitialValue((vComponent, type), initialValue);
            }

            var currentValue = default(TValue);

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

            var handle = motionBuilder.BindWithState(vComponent, (x, target) => SetValue(target, x));
            builder.Add(handle);
        }

        public override void RestoreValues(ISequencePropertyTable propertyTable)
        {
            var profile = ResolveTarget(propertyTable);
            if (profile == null) return;

            var target = profile.sharedProfile;
            if (!target.TryGet<TVolumeComponent>(out var vComponent)) return;

            if (propertyTable.TryGetInitialValue((vComponent, GetType()), out TValue initialValue))
            {
                SetValue(vComponent, initialValue);
            }
        }

        static class TempData
        {
            public static VolumeProfile defaultProfile;
            public static VolumeProfile modifiedProfile;
            public static Volume profile;
        }

        protected override void ConfigureMotionBuilder<TValue1, TOptions1, TAdapter1>(ref MotionBuilder<TValue1, TOptions1, TAdapter1> motionBuilder)
        {
            base.ConfigureMotionBuilder(ref motionBuilder);

            var def = TempData.defaultProfile;
            var mod = TempData.modifiedProfile;
            var profile = TempData.profile;

            Action action = () =>
            {
                profile.sharedProfile = def;
#if UNITY_EDITOR
                if (EditorApplication.isPlaying) Destroy(mod);
                else DestroyImmediate(mod);
#else
                Destroy(mod);
#endif
            };

            motionBuilder.WithOnComplete(action)
                .WithOnCancel(action);
        }
    }
}

#endif