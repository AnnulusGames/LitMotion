# Custom Animation Components

By inheriting the `LitMotionAnimationComponent` class, you can create custom Animation Components that can be used in LitMotion Animation.

```cs
[Serializable]
// The menu name displayed in the dropdown when pressing [Add...] can be specified using the LitMotionAnimationComponentMenu attribute
[LitMotionAnimationComponentMenu("Custom/Custom Animation")]
public class CustomAnimation : LitMotionAnimationComponent
{
    public override MotionHandle Play()
    {
        // Actions to perform when Play() is called and the animation starts
        // Return the created MotionHandle
    }

    public override void OnPause()
    {
        // Actions to perform when Pause() is called
    }

    public override void OnResume()
    {
        // Actions to perform when Play() is called after a pause and the animation resumes
    }

    public override void OnStop()
    {
        // Actions to perform when Stop() is called
    }
}
```

> [!WARNING]
> When implementing a `LitMotionAnimationComponent`, you need to manually implement the value restoration logic in `OnStop()` when the animation finishes.

## PropertyAnimationComponent

To simplify the implementation of `LitMotionAnimationComponent`, a class called `PropertyAnimationComponent<TObject, TValue, TOptions, TAdapter>` is provided. By inheriting this, you can easily implement an Animation Component without writing code for motion creation or value restoration.

Additionally, to simplify the generic type arguments, types like `FloatPropertyAnimationComponent<TObject>`, `Vector3PropertyAnimationComponent<TObject>`, and others are available.

The following is an implementation of an Animation Component that animates the `Slider.value`.

```cs
[Serializable]
[LitMotionAnimationComponentMenu("UI/Slider/Value")]
public sealed class SliderValueAnimation : FloatPropertyAnimationComponent<Slider>
{
    protected override float GetValue(Slider target) => target.value;
    protected override void SetValue(Slider target, in float value) => target.value = value;
}
```
