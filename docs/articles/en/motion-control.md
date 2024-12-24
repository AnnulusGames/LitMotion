# Motion Control

Methods for creating motions, such as `Bind()` and `RunWithoutBinding()`, all return a `MotionHandle` struct.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
```

You can control the motion through this struct.

### Completing/Canceling Motion

To complete a running motion, call `Complete()`.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.Complete();
```

To cancel a running motion, call `Cancel()`.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.Cancel();
```

### Motion Existence Check

The above methods/properties will throw an exception if the motion has already finished or if the `MotionHandle` is uninitialized. To check if the motion the `MotionHandle` points to exists, use `IsActive()`.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();

if (handle.IsActive()) handle.Complete();
```

If you want to call `Complete()` or `Cancel()` only if the motion exists, you can simplify the code using `TryComplete()` / `TryCancel()`.

```cs
handle.TryComplete();
handle.TryCancel();
```

### Reusing Motion

By default, completed motions are automatically discarded, so `MotionHandle` cannot be reused.

If you want to reuse the same motion, call `Preserve()`. This prevents the motion from being discarded after completion.

```cs
// Call Preserve()
handle.Preserve();

// It can be reused after completion
handle.Complete();
handle.Time = 0;
```

However, motions that have called `Preserve()` will continue to run until `Cancel()` is explicitly called. After use, you should either call `Cancel()` or link the lifetime to a GameObject using `AddTo()`.

### Adjusting Playback Speed

You can modify the playback speed of a motion using the `MotionHandle.PlaybackSpeed` property. This allows you to slow down, reverse, or pause the motion.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.PlaybackSpeed = 2f;
```

### Manually Controlling Motion

You can manually control a motion using the `Time` property.

```cs
// Manually set the elapsed time of the motion
handle.Time = 0.5;
```

However, motions that are completed by manually adjusting `Time` are also automatically discarded by default. If you want to manually control a motion, it's recommended to call `Preserve()`.

### Getting Motion Information

You can get motion data from the properties of the `MotionHandle`.

```cs
// Duration per loop
var duration = handle.Duration;

// Total duration of the motion
var totalDuration = handle.TotalDuration;

// Delay time
var delay = handle.Delay;

// Number of loops
var loops = handle.Loops;

// Number of completed loops
var completedLoops = handle.CompletedLoops;
```

To check if a motion is currently playing, use `IsPlaying()`. This is similar to `IsActive()`, but unlike `IsActive()`, which always returns `true` until the motion is discarded, `IsPlaying()` will return `false` once the motion completes (if `Preserve()` was called).

```cs
if (handle.IsPlaying())
{
    DoSomething();
}
```

### Linking Cancelation to GameObject

You can use `AddTo()` to automatically cancel the motion when the GameObject is destroyed.

```cs
LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(this.gameObject);
```

### Managing Multiple `MotionHandle`

To manage multiple `MotionHandle` instances together, the `CompositeMotionHandle` class is provided. You can bind `MotionHandle` instances to this class using `AddTo()`.

```cs
var handles = new CompositeMotionHandle();

LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(handles);
```
