# Updating Motion Manually

You can configure the motion's update process to be manual by specifying `MotionScheduler.Manual` as the scheduler.

```cs
// Specify MotionScheduler.Manual as the scheduler
var handle = LMotion.Create(value, endValue, 2f)
    .WithScheduler(MotionScheduler.Manual)
    .BindToUnityLogger();
```

For motions with `MotionScheduler.Manual`, you need to manually update the motion using `ManualMotionDispatcher.Update()`.

```cs
while (handle.IsActive())
{
    var deltaTime = 0.1f;
    // Update using ManualMotionDispatcher.Update()
    ManualMotionDispatcher.Update(deltaTime);
}
```

Also, when you have disabled Domain Reload, the motion's state might not initialize, leading to unexpected behavior. To avoid this, explicitly initialize it at startup using `ManualMotionDispatcher.Reset()`.

```cs
void Awake()
{
    ManualMotionDispatcher.Reset();
}
```