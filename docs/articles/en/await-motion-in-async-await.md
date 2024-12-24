# Await Motion in async/await

`MotionHandle` implements the `GetAwaiter()` method, allowing you to directly await it to wait for completion.

```cs
await handle;
```

If you want to pass a `CancellationToken`, you can use `ToValueTask()` / `ToAwaitable()` or UniTask.

## ValueTask

You can convert a motion to a `ValueTask` using `MotionHandle.ToValueTask()`. This allows you to use async/await to wait for the completion of the motion.

```cs
async ValueTask ExampleAsync(CancellationToken cancellationToken)
{
    await LMotion.Create(0f, 10f, 1f)
        .RunWithoutBinding()
        .ToValueTask(cancellationToken);
}
```

However, using `ValueTask` in Unity has some performance concerns. For the best performance, it is recommended to use UniTask. For integration with UniTask, refer to the [UniTask](integration-unitask.md) guide.

### Awaitable

Starting from Unity 2023.1, Unity provides the [Awaitable](https://docs.unity3d.com/2023.1/Documentation/ScriptReference/Awaitable.html) class to enable efficient async/await handling within Unity.

If you're using Unity 2023.1 or later, LitMotion provides the `ToAwaitable()` extension method to convert a `MotionHandle` to an `Awaitable`. This allows you to use async/await to wait for motion completion.

```cs
async Awaitable ExampleAsync(CancellationToken cancellationToken)
{
    await LMotion.Create(0f, 10f, 1f)
        .RunWithoutBinding()
        .ToAwaitable(cancellationToken);
}
```

### Changing Behavior on Cancellation

By specifying `CancelBehavior` in the arguments for `ToValueTask()` / `ToAwaitable()`, you can change the behavior when the async method is canceled. Additionally, setting `cancelAwaitOnMotionCanceled` to `true` allows the async method to be canceled when the `MotionHandle` is canceled.

```cs
await LMotion.Create(0f, 10f, 1f)
    .RunWithoutBinding()
    .ToAwaitable(CancelBehavior.Complete, true, cancellationToken);
```
