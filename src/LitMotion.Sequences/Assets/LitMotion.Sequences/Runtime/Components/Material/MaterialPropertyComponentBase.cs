using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace LitMotion.Sequences
{
    public abstract class MaterialPropertyComponentBase<T> : SequenceComponentBase<T, Renderer>
        where T : unmanaged
    {
        [Header("Material Property Settings")]
        [SerializeField] string propertyName;

        public override void ResetComponent()
        {
            base.ResetComponent();
            propertyName = "";
        }

        protected abstract T GetValue(Material material, string propertyName);
        protected abstract void SetValue(Material material, string propertyName, T value);
        protected abstract MotionHandle CreateMotion(Material material, string propertyName, T current);

        public override void Configure(ISequencePropertyTable propertyTable, SequenceItemBuilder builder)
        {
            var renderer = ResolveTarget(propertyTable);
            if (renderer == null) return;

            var target = new Material(renderer.sharedMaterial);
#if UNITY_EDITOR
            target.name += " (Instance)";
#endif

            TempData.defaultMaterial = renderer.sharedMaterial;
            TempData.modifiedMaterial = target;
            TempData.renderer = renderer;

            renderer.sharedMaterial = target;

            var type = GetType();
            if (!propertyTable.TryGetInitialValue<(Material, Type), T>((target, type), out var initialValue))
            {
                initialValue = GetValue(target, propertyName);
                propertyTable.SetInitialValue((target, type), initialValue);
            }

            var currentValue = default(T);

            switch (MotionMode)
            {
                case MotionMode.Relative:
                    currentValue = initialValue;
                    break;
                case MotionMode.Additive:
                    currentValue = GetValue(target, propertyName);
                    break;
            }

            var handle = CreateMotion(target, propertyName, currentValue);
            builder.Add(handle);
        }

        public override void RestoreValues(ISequencePropertyTable propertyTable)
        {
            var renderer = ResolveTarget(propertyTable);
            if (renderer == null) return;

            var target = renderer.sharedMaterial;

            if (propertyTable.TryGetInitialValue<(Material, Type), T>((target, GetType()), out var initialValue))
            {
                SetValue(target, propertyName, initialValue);
            }
        }

        static class TempData
        {
            public static Material defaultMaterial;
            public static Material modifiedMaterial;
            public static Renderer renderer;
        }

        protected override void ConfigureMotionBuilder<TValue, TOptions, TAdapter>(ref MotionBuilder<TValue, TOptions, TAdapter> motionBuilder)
        {
            base.ConfigureMotionBuilder(ref motionBuilder);

            var def = TempData.defaultMaterial;
            var mod = TempData.modifiedMaterial;
            var renderer = TempData.renderer;

            Action action = () =>
            {
                renderer.sharedMaterial = def;
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
