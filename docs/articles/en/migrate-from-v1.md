# Migrate from LitMotion v1 to v2

In v2, along with the addition of new features, some of the API has been refreshed. As a result, there are several changes you need to account for when migrating from v1.

## Motion Reuse

In v1, the `Preserve()` method could be used on the `MotionBuilder`, but this has been deprecated in v2.

Instead, `MotionHandle.Preserve()` has been added. When `Preserve()` is called on a `MotionHandle`, the motion will not become invalid after completion, allowing you to reuse the same motion. However, you will need to explicitly call `Cancel()` to discard the motion when done.

Additionally, if you want to reuse the same motion settings, you can use `MotionSettings<T, TOptions>`.

## `Bind` Extension Methods Relocated

The extension methods `BindToUnityLogger()` and `BindToProgress()` have been moved to the `LitMotion.Extensions` namespace.

To use these extension methods, you'll need to reference `LitMotion.Extensions.asmdef` and include `using LitMotion.Extensions;`.

## `BindWithState()` -> `Bind()`

In v1, `BindWithState()` was used to avoid closures. This has now been changed to an overload of `Bind()`, which accepts a struct `State` in v2. While this introduces some allocation overhead, it is more efficient than closures in terms of allocation and call speed.

```cs
// v1
LMotion.Create(0f, 1f, 1f)
    .BindWithState(state, (x, state) => { });

// v2
LMotion.Create(0f, 1f, 1f)
    .Bind(state, (x, state) => { });
```

## `ToYieldInteraction()` -> `ToYieldInstruction()`

The method used to wait for a `MotionHandle` in a coroutine has changed from `ToYieldInteraction()` to `ToYieldInstruction()`. This was corrected in v2, as the naming in v1 was inaccurate.

## Behavior of `LoopType.Yoyo`

In v1, `LoopType.Yoyo` did not behave as expected based on its naming. This has been fixed in v2. If you want to retain the old behavior, use the new `LoopType.Flip` in v2.

![Image showing LoopType.Yoyo behavior change](#TODO-image)

## Enum Naming Changes

In v2, some enums have been renamed to follow American English spelling conventions:

- `LinkBehaviour` -> `LinkBehavior`
- `CancelBehaviour` -> `CancelBehavior`

## `WithBindOnSchedule()` -> `WithImmediateBind()`

In v1, `WithBindOnSchedule()` has been renamed to `WithImmediateBind()`. Additionally, its default value has been changed to `true`, so there's no need to call it explicitly unless you want to change this behavior.

```cs
// v1
LMotion.Create(0f, 1f, 1f)
    .WithBindOnSchedule()
    .BindToPositionX(transform);

// v2
LMotion.Create(0f, 1f, 1f)
    .WithImmediateBind() // Default is true, so there's no need to call it explicitly.
    .BindToPositionX(transform);
```

## ManualMotionDispatcher

In v2, the `ManualMotionDispatcher` class has been changed to a regular class. This allows you to create multiple instances of `ManualMotionDispatcher`.

When migrating from v1, you can use the `ManualMotionDispatcher.Default` instance, which is globally available.

```cs
// v1
ManualMotionDispatcher.Update(0.1);

// v2
ManualMotionDispatcher.Default.Update(0.1);
```

In v2, you can also retrieve the `IMotionScheduler` from `dispatcher.Scheduler` to schedule motions. `MotionScheduler.Manual` remains available as an alias for `ManualMotionDispatcher.Default.Scheduler`.

## MotionTracker Window Deprecated

The `MotionTracker` window that existed in v1 has been deprecated. In v2, a more powerful tool, the [LitMotion Debugger](litmotion-debugger.md), has been added. It is recommended to use the LitMotion Debugger for tracking motion-related information. 
