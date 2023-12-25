# UniTask

Introducing [UniTask](https://github.com/Cysharp/UniTask) into your project adds extension methods that allow motion waiting to be compatible with async/await.

### Waiting for Motion

By integrating UniTask, you can await `MotionHandle`:

```cs
await LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x));
```

You can also pass a `CancellationToken` using the `ToUniTask()` method:

```cs
var cts = new CancellationTokenSource();
await LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x))
    .ToUniTask(cts.Token);
```

Passing a CancellationToken automatically cancels motion playback when asynchronous processing is canceled.
