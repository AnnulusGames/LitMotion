# IDisposableに変換する

`MotionHandle`は`IDisposable`に変換することが可能です。

```cs
var disposable = handle.ToDisposable();
```

デフォルトでは、この`IDisposable`に対して`Dispose()`を呼び出すとモーションがキャンセルされます。この挙動は`ToDisposable()`の引数に`DisposeBehavior`を指定することで変更できます。

```cs
var disposable = handle.ToDisposable(DisposeBehavior.Complete);

// handle.Complate()が呼ばれる
disposable.Dispose();
```