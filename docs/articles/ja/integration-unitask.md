# UniTask

プロジェクトに[UniTask](https://github.com/Cysharp/UniTask)を導入することで、モーションの待機をasync/awaitに対応させる拡張メソッドが追加されます。

UniTaskをPackage Managerから導入した場合は自動で以下の機能が追加されます。unitypackage等で導入した場合は、`Project Settings > Player > Scripting Define Symbols`に`LITMOTION_UNITASK_SUPPORT`を追加する必要があります。

### モーションの待機

UniTaskを導入することで、`MotionHandle`をawaitで待機することが可能になります。

```cs
await LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x));
```

`ToUniTask()`メソッドを使用することで`CancellationToken`を渡すことも可能です。

```cs
var cts = new CancellationTokenSource();
await LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x))
    .ToUniTask(cts.Token);
```

CancellationTokenを渡すと、非同期処理がキャンセルされた際に自動でモーションの再生もキャンセルされます。