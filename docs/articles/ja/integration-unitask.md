# UniTask

プロジェクトに[UniTask](https://github.com/Cysharp/UniTask)を導入することで、モーションの待機をasync/awaitに対応させる拡張メソッドが追加されます。

UniTaskをPackage Managerから導入した場合は自動で以下の機能が追加されます。unitypackage等で導入した場合は、`Project Settings > Player > Scripting Define Symbols`に`LITMOTION_SUPPORT_UNITASK`を追加する必要があります。

### モーションの待機

`ToUniTask()`メソッドを使用することで`MotionHandle`を`UniTask`に変換できます。

```cs
var cts = new CancellationTokenSource();
await LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x))
    .ToUniTask(cts.Token);
```

引数で指定できるオプションについては`ToValueTask()` / `ToAwaitable()`と同じです。
