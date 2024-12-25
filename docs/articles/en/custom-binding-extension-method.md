# Custom Binding Extension Method

Although it is possible to animate properties not provided in `LitMotion.Extensions` using `Bind()`, it can be convenient to define extension methods for frequently used properties. Here, we will demonstrate how to add a custom binding extension method to `MotionBuilder`.

As an example, let's define an extension method to bind a motion to the `Value` property of a `Foo` class like the one below:

```cs
public class Foo
{
    public float Value { get; set; }
}
```

Define an extension method tailored to the type of the target property (in this case, float) as follows:

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

By defining it as a generic method, you can bind values of float type regardless of the types of `TOptions` and `TAdapter`. Additionally, when the target is a class, using `Bind(TState, Action<T, TState>)` avoids the use of closures, allowing you to create motions with zero allocation.

The code to animate a value using this extension method is below.

```cs
var foo = new Foo();
LMotion.Create(0f, 10f, 2f)
    .BindToFooValue(foo);
```