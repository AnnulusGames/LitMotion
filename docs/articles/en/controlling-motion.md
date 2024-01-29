# Controlling Motion

Methods that create motions like `Bind()` or `RunWithoutBinding()` all return a `MotionHandle` structure.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
```

You can control motions through this structure.

### Motion Completion/Cancellation

To complete a motion in progress, call `Complete()`.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.Complete();
```

To cancel a motion in progress, call `Cancel()`.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.Cancel();
```

### Motion Playback Speed

You can change the playback speed of a motion using the `MotionHandle.PlaybackSpeed` property. This allows you to perform actions such as slow-motion or pause during the motion playback.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.PlaybackSpeed = 2f;
```

> [!WARNING]
> PlaybackSpeed does not support values less than 0. Trying to set a negative value to `MotionHandle.PlaybackSpeed` will throw an exception.

### Motion Existence Check

The methods and properties mentioned above will throw exceptions if the motion has already ended or if the `MotionHandle` has not been initialized. To check whether the motion pointed to by the `MotionHandle` exists, use `IsActive()`.

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();

if (handle.IsActive()) handle.Complete();
```

### Linking Cancellation to GameObject

Using `AddTo()`, you can automatically cancel motions when the GameObject is destroyed.

```cs
LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(this.gameObject);
```

### Managing Multiple MotionHandles Together

For managing multiple `MotionHandle`s, the `CompositeMotionHandle` class is provided. You can associate MotionHandles with it using `AddTo()`.

```cs
var handles = new CompositeMotionHandle();

LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(handles);
```

---
If you need further assistance or have more questions, feel free to ask!