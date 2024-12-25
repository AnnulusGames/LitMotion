# MotionSettings

`MotionSettings<T, TOptions>`を用いることで、モーションの設定を保持しておくことが可能になります。

`MotionSettings<T, TOptions>`はオブジェクト初期化子を用いて作成できるほか、`MotionBuilder`から作成することも可能です。型引数にはアニメーションさせる値の型とオプションの型(`NoOptions`, `IntegerOptions`, `StringOptions`, `PunchOptions`, `ShakeOptions`, etc...)を渡します。

```cs
// オブジェクト初期化子を用いて作成
var settings = new MotionSettings<float, NoOptions>
{
    StartValue = 0f,
    EndValue = 10f,
    Duration = 2f,
    Ease = Ease.OutQuad
};

// MotionBuilderを用いて作成
var settings = LMotion.Create(0f, 10f, 2f)
    .WithEase(Ease.OutQuad)
    .ToMotionSettings();
```

作成した`MotionSettings<T, TOptions>`は`LMotion.Create()`の引数として渡すことが可能です。

```cs
LMotion.Create(settings)
    .Bind(x => { });
```

また`MotionSettings<T, TOptions>`はrecord型であるため、設定の一部のみをwith式で上書きすることも可能です。

```cs
var newSettings = settings with
{
    StartValue = 5f
};
```

## SerializableMotionSettings

`MotionSettings<T, TOptions>`の代わりに`SerializableMotionSettings<float, NoOptions>`を利用することで、Inspectorから値を編集することが可能になります。

```cs
public class Example : MonoBehaviour
{
    [SerializeField] SerializableMotionSettings<float, NoOptions> settings;

    void Start()
    {
        LMotion.Create(settings)
            .BindToPositionX(transform);
    }
}
```

![img](../../images/img-serializable-motion-settings-inspector.png)
