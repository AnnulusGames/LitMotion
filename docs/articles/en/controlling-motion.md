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

### Motion Existence Check

The mentioned methods throw exceptions if the motion has already ended or if the `MotionHandle` hasn’t been initialized. Use `IsActive()` to check if the motion referenced by `MotionHandle` exists.

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