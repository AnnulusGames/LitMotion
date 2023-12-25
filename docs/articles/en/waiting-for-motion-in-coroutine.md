# Waiting for Motion in Coroutine

It's possible to await the completion of a motion within a coroutine using `ToYieldInteraction()` of `MotionHandle`.

```cs
IEnumerator CoroutineExample()
{
    yield return LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x))
        .ToYieldInteraction();
}
```

However, coroutines have functional limitations such as the inability to return values and await in parallel due to language constraints.

As an alternative, it's recommended to use async/await with UniTask for waiting on motions. For information on integrating with UniTask, please refer to [UniTask Integration](integration-unitask.md).
