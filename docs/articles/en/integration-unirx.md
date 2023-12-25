# UniRx

By integrating [UniRx](https://github.com/neuecc/UniRx) into your project, it adds extension methods compatible with Reactive Extensions (Rx).

### Creating Motion as an Observable

You can create motion as an `IObservable<T>` using the `ToObservable()` method:

```cs
var observable = LMotion.Create(0f, 5f, 2f).ToObservable();
observable.Subscribe(x =>
{
    Debug.Log(x);
})
.AddTo(gameObject);
```

### Binding to ReactiveProperty

There's a provided extension method to bind the created motion to a `ReactiveProperty<T>`:

```cs
var reactiveProperty = new ReactiveProperty<float>();
LMotion.Create(0f, 10f, 2f)
    .BindToReactiveProperty(reactiveProperty);
```
