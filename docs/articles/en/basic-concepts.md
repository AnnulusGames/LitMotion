# Basic Concepts

The LitMotion API consists of several components that are crucial to understand when using LitMotion.

### MotionBuilder

A structure used to construct motions. `LMotion.Create()` returns a `MotionBuilder` instance.

```cs
var builder = LMotion.Create(0f, 10f, 3f);
```

MotionBuilder provides methods to configure motions, allowing method chaining.

```cs
LMotion.Create(0f, 10f, 3f)
    .WithEase(Ease.OutQuad)
    .WithDelay(2f)
    .WithLoops(4, LoopType.Yoyo);
```

LitMotion motions are typically bound to some value. Binding triggers motion creation and playback.

```cs
var value = 0f;
LMotion.Create(0f, 10f, 3f)
    .WithEase(Ease.OutQuad)
    .Bind(x => value = x);
```

For more details, refer to [Binding](binding.md) and [Motion Configuration](motion-configuration.md).

### MotionHandle

`MotionHandle` controls created motions. Methods like `Bind()` from MotionBuilder return this handle.

```cs
var handle = LMotion.Create(0f, 10f, 3f).Bind(x => value = x);
```

You can manage motion presence, completion, or cancellation through MotionHandle.

```cs
var handle = LMotion.Create(0f, 10f, 3f).Bind(x => value = x);

if (handle.IsActive())
{
    handle.Complete();
    handle.Cancel();
}
```

Additionally, tying cancellation timing to a GameObject is possible using `AddTo()`.

```cs
LMotion.Create(0f, 10f, 3f)
    .Bind(x => value = x)
    .AddTo(this.gameObject);
```

Use `CompositeMotionHandle` to manage multiple MotionHandles together.

```cs
var handles = new CompositeMotionHandle();

LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(handles);
```

For further details, refer to [Controlling Motion](controlling-motion.md).

### MotionScheduler

Motion update timing is determined by MotionScheduler. Set the Scheduler using `WithScheduler()`.

```cs
LMotion.Create(0f, 10f, 2f)
    .WithScheduler(MotionScheduler.FixedUpdate)
    .Bind(() => Debug.Log(x));
```

Refer to [Motion Configuration](motion-configuration.md) for more information.

### IMotionAdapter

The interpolation between two values is described by structures implementing `IMotionAdapter<T, TOptions>`. Built-in adapters are defined within the `LitMotion.Adapters` namespace.

To add specific options to a motion, define a structure implementing `IMotionOptions`.

Refer to [Custom Adapter](custom-adapter.md) for creating custom adapters.
