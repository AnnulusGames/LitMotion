# UniTask

Introducing [UniTask](https://github.com/Cysharp/UniTask) into your project adds extension methods that allow motion waiting to be compatible with async/await.

If you have installed UniTask via the Package Manager, the following functionality will be automatically added. However, if you have imported UniTask via a unitypackage or similar method, you'll need to add `LITMOTION_SUPPORT_UNITASK` to `Settings > Player > Scripting Define Symbols`.

### Waiting for Motion

You can convert a `MotionHandle` to a `UniTask` using the `ToUniTask()` method.

```cs
var cts = new CancellationTokenSource();
await LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x))
    .ToUniTask(cts.Token);
```

The options that can be specified in the argument are the same as for `ToValueTask()` / `ToAwaitable()`.
