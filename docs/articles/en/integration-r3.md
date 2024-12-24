# R3

By integrating [R3](https://github.com/Cysharp/R3) into your project, it adds extension methods compatible with Reactive Extensions (Rx).

If you have installed R3 via the Package Manager, the following functionality will be automatically added. However, if you have imported UniRx via a unitypackage or similar method, you'll need to add `LITMOTION_SUPPORT_R3` to `Settings > Player > Scripting Define Symbols`.

### Creating Motion as an Observable

You can create motion as an `Observable<T>` using the `ToObservable()` method:

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
