# Animation Componentを作成する

`LitMotionAnimationComponent`を継承したクラスを実装することで、LitMotion Animationで利用可能なAnimation Componentを自作することができます。

```cs
[Serializable]
// LitMotionAnimationComponentMenu属性で[Add...]を押した際に表示されるドロップダウンのメニュー名を指定できる
[LitMotionAnimationComponentMenu("Custom/Custom Animation")]
public class CustomAnimation : LitMotionAnimationComponent
{
    public override MotionHandle Play()
    {
        // ここにPlay()で再生が開始した処理
        // 作成したMotionHandleを返す
    }

    public override void OnPause()
    {
        // Pause()で一時停止した際のの処理
    }

    public override void OnResume()
    {
        // Pause()後にPlay()で再開した際の処理
    }

    public override void OnStop()
    {
        // Stop()で再生が終了した際の処理
    }
}
```

> [!WARNING]
> LitMotionAnimationComponentを実装する場合、再生終了時の値の復元処理はOnStop()で自前で実装する必要があります。

## PropertyAnimationComponent

`LitMotionAnimationComponent`の実装を簡略化するためのクラスとして、`PropertyAnimationComponent<TObject, TValue, TOptions, TAdapter>`が用意されています。これを継承することで、モーションの作成と値の復元の処理を書くことなく、簡単にAnimation Componentを実装できます。

また、ジェネリクスの型引数を簡略化するために`FloatPropertyAnimationComponent<TObject>`, `Vector3PropertyAnimationComponent<TObject>`などの型も用意されています。

以下は`Slider.value`をアニメーションさせるAniamtion Componentの実装です。

```cs
[Serializable]
[LitMotionAnimationComponentMenu("UI/Slider/Value")]
public sealed class SliderValueAnimation : FloatPropertyAnimationComponent<Slider>
{
    protected override float GetValue(Slider target) => target.value;
    protected override void SetValue(Slider target, in float value) => target.value = value;
}
```