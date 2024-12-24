# ManualMotionDispatcher

By using the `ManualMotionDispatcher`, you can manually update multiple motions.

```cs
var dispatcher = new ManualMotionDispatcher();
```

You can set the motion update process to be handled by the created dispatcher by specifying `dispatcher.Scheduler` in the Scheduler.

```cs
// Set the Scheduler to the created ManualMotionDispatcher's Scheduler
var handle = LMotion.Create(value, endValue, 2f)
    .WithScheduler(dispatcher.Scheduler)
    .BindToUnityLogger();
```

You can perform updates using `dispatcher.Update(double deltaTime)`.

```cs
dispatcher.Update(0.1);
```

## ManualMotionDispatcher.Default

If you need a globally available `ManualMotionDispatcher`, you can use `ManualMotionDispatcher.Default`.

```cs
ManualMotionDispatcher.Default.Update(0.1);
```

`MotionScheduler.Manual` is equivalent to `ManualMotionDispatcher.Default.Scheduler`.

> [!WARNING]
> When using `ManualMotionDispatcher.Default`, setting Domain Reload to off may cause unexpected behavior, as the motion state is not reset. To avoid this, explicitly initialize it at startup by calling `Reset()`.
> ```cs
> void Awake()
> {
>     ManualMotionDispatcher.Default.Reset();
> }
> ```
