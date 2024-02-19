# R3

プロジェクトに[R3](https://github.com/Cysharp/R3)を導入することでReactive Extensions(Rx)に対応した拡張メソッドが追加されます。

R3をPackage Managerから導入した場合は自動で以下の機能が追加されます。unitypackage等で導入した場合は、`Project Settings > Player > Scripting Define Symbols`に`LITMOTION_SUPPORT_R3`を追加する必要があります。

### モーションをObservableとして作成

`ToObservable()`を使用することで、モーションを`IObservable<T>`として作成できます。

```cs
var observable = LMotion.Create(0f, 5f, 2f).ToObservable();
observable.Subscribe(x =>
{
    Debug.Log(x);
})
.AddTo(gameObject);
```

### ReactivePropertyへバインド

作成したモーションを`ReactiveProperty<T>`にバインドする拡張メソッドが用意されています。

```cs
var reactiveProperty = new ReactiveProperty<float>();
LMotion.Create(0f, 10f, 2f)
    .BindToReactiveProperty(reactiveProperty);
```
