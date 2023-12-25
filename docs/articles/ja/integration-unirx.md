# UniRx

プロジェクトに[UniRx](https://github.com/neuecc/UniRx)を導入することでReactive Extensions(Rx)に対応した拡張メソッドが追加されます。

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
