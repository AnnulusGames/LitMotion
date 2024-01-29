# Waiting for Motion in async/await

`MotionHandle.ToValueTask()` allows you to convert a motion to a `ValueTask`. This enables you to await the completion of the motion using async/await.

```cs
async ValueTask ExampleAsync(CancellationToken cancellationToken)
{
    await LMotion.Create(0f, 10f, 1f)
        .RunWithoutBinding()
        .ToValueTask(cancellationToken);
}
```

However, using `ValueTask` in Unity may have performance implications. For optimal performance, it is recommended to use UniTask. Refer to [UniTask](integration-unitask.md) for information on integrating UniTask with LitMotion.

### Awaitable

Unity 2023.1 and later versions provide the [Awaitable](https://docs.unity3d.com/2023.1/Documentation/ScriptReference/Awaitable.html) class, which allows for efficient async/await in Unity.

If you're using Unity 2023.1 or a later version, LitMotion provides an extension method `ToAwaitable()` to convert a `MotionHandle` into an `Awaitable`. This allows you to await the motion using async/await.

```cs
async Awaitable ExampleAsync(CancellationToken cancellationToken)
{
    await LMotion.Create(0f, 10f, 1f)
        .RunWithoutBinding()
        .ToAwaitable(cancellationToken);
}
```