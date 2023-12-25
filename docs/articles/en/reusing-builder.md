# Reusing MotionBuilder

MotionBuilder automatically return the buffer to the pool when creating a motion, preventing the creation of multiple motions from the same builder. For example, the following code will throw an exception:

```cs
var builder = LMotion.Create(0f, 10f, 2f);
builder.BindToUnityLogger();
builder.BindToUnityLogger(); // Throws an InvalidOperationException
```

If you want to create multiple motions using the same builder, you need to call `Preserve()` to retain the internal values.

Additionally, when using `Preserve()`, the builder won't automatically return the buffer to the pool. Therefore, it's necessary to explicitly call `Dispose()` to properly dispose of the builder (using a `using` statement is recommended to prevent forgetting to call `Dispose()`).

```cs
using var builder = LMotion.Create(0f, 10f, 2f).Preserve();
builder.BindToUnityLogger();
builder.BindToUnityLogger();
```
