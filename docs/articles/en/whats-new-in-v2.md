# What's new in v2

LitMotion v2 introduces significant enhancements to its functionality. In this section, we will introduce the features added in v2.

For migration from v1, refer to [Migrate from LitMotion v1](./migrate-from-v1.md).

## LitMotion.Animation Package

The LitMotion.Animation package has been added, which provides functionality for creating animations directly from the Inspector. This package is separate from the main LitMotion package and, when integrated into your project, it enables the use of the LitMotion Animation component, which allows you to create animations through a visual editor in the Unity Inspector.

![img](../../images/img-litmotion-animation.gif)

For more details, see the [LitMotion.Animation](./litmotion-animation-overview.md) section.

## Sequence

The Sequence feature has been added to compose multiple motions. You can create a builder with `LSequence.Create()` and add motions to it.

```cs
LSequence.Create()
    .Append(LMotion.Create(-5f, 5f, 0.5f).BindToPositionX(target))
    .Append(LMotion.Create(0f, 5f, 0.5f).BindToPositionY(target))
    .Append(LMotion.Create(-2f, 2f, 1f).BindToPositionZ(target))
    .Run();
```

For more details, refer to the [Sequence](./sequence.md) section.

## MotionHandle

The following properties and methods have been added to `MotionHandle`:

```cs
MotionHandle handle;

// methods
handle.Preserve();
handle.IsPlaying();
handle.TryComplete();
handle.TryCancel();

// readonly properties
handle.Duration;
handle.TotalDuration;
handle.Loops;
handle.CompletedLoops;
handle.Delay;

// properties
handle.Time;
```

## WithOnLoopComplete

A new callback has been added that is triggered upon the completion of each loop. You can add this callback using `WithOnLoopComplete()`.

```cs
LMotion.Create(...)
    .WithOnLoopComplete(completedLoops => { })
    .Bind(x => { })
```

## LitMotion Debugger

In place of the MotionTracker window from v1, we have added the more powerful LitMotion Debugger. Additionally, new debugging APIs have been introduced.

![img](../../images/img-litmotion-debugger.png)

For more details, refer to the [LitMotion Debugger](./litmotion-debugger.md) section.
