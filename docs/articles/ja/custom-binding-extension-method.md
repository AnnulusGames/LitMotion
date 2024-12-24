# Bind拡張メソッドを作成する

`LitMotion.Extensions`で用意されていないプロパティは`Bind()`を用いてアニメーションさせることが可能ですが、多用するものに関しては拡張メソッドを定義した方が便利な場合もあります。ここでは、`MotionBuilder`に独自のBind拡張メソッドを追加する方法を示します。

例として、以下のような`Foo`クラスの`Value`プロパティにモーションをバインドする拡張メソッドを定義してみましょう。

```cs
public class Foo
{
    public float Value { get; set; }
}
```

対象のプロパティの型(ここではfloat型)に合わせて以下のような拡張メソッドを定義します。

```cs
public static class FooMotionExtensions
{
    public static MotionHandle BindToFooValue<TOptions, TAdapter>(this MotionBuilder<float, TOptions, TAdapter> builder, Foo target)
        where TOptions : unmanaged, IMotionOptions
        where TAdapter : unmanaged, IMotionAdapter<float, TOptions>
    {
        return builder.Bind(target, (x, target) =>
        {
            target.Value = x;
        });
    }
}
```

ジェネリックメソッドとして定義することにより、float型の値であれば`TOptions`や`TAdapter`の型が何であってもバインドを行うことが可能になります。また、対象がクラスの場合は`Bind(TStete, Action<T, TState>)`のオーバーロードを利用することでクロージャを避け、ゼロアロケーションでモーションを作成できます。

実際にこれを使用して値をアニメーションさせるコードは以下のようになります。

```cs
var foo = new Foo();
LMotion.Create(0f, 10f, 2f)
    .BindToFooValue(foo);
```
