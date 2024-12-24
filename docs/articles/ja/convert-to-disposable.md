# IDisposableに変換する

`MotionHandle`は`IDisposable`に変換することが可能です。

```cs
var disposable = handle.ToDisposable();
```

デフォルトでは、この`IDisposable`に対して`Dispose()`を呼び出すとモーションがキャンセルされます。この挙動は`ToDisposable()`の引数に`MotionDisposeBehavior`を指定することで変更できます。

```cs
var disposable = handle.ToDisposable(MotionDisposeBehavior.Complete);

// handle.Complate()が呼ばれる
disposable.Dispose();
```