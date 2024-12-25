# Convert to `IDisposable`

`MotionHandle` can be converted to `IDisposable`.

```cs
var disposable = handle.ToDisposable();
```

By default, calling `Dispose()` on this `IDisposable` will cancel the motion. This behavior can be changed by specifying a `MotionDisposeBehavior` argument in `ToDisposable()`.

```cs
var disposable = handle.ToDisposable(MotionDisposeBehavior.Complete);

// handle.Complete() will be called
disposable.Dispose();
```
