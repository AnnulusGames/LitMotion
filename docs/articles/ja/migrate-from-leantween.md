# LeanTweenから移行する

このページでは、LeanTweenからLitMotionへ移行するための簡易的な対応を示します。

## 位置のトゥイーン

```cs
var endValue = new Vector3(1f, 2f, 3f);
var duration = 1f;

// LeanTween
LeanTween.move(gameObject, endValue, duration);

// LitMotion
LMotion.Create(tranform.position, endValue, duration)
    .BindToPosition(transform);
```

## 値のトゥイーン

```cs
// LeanTween
var value = 0f;
LeanTween.value(gameObject, x => value = x, value, endValue, duration);

// LitMotion
LMotion.Create(value, endValue, duration)
    .Bind(x => value = x);
```

## From

```cs
// LeanTween
LeanTween.moveX(gameObject, endValue, duration)
    .from(startValue);

// LitMotion
LMotion.Create(startValue, endValue, duration)
    .BindToPositionX(transform);
```

## トゥイーンの設定

```cs
// LeanTween
descr.setRepeat(2)
    .setLoopPingPong();
    .setEase(LeanTweenType.easeOutQuad);

// LitMotion
builder.WithLoops(2, LoopType.Flip)
    .WithEase(Ease.OutQuad);
```

## トゥイーンの制御

```cs
// LeanTween
descr.pause();
descr.cancel();

// LitMotion
handle.PlaybackSpeed = 0f;
handle.Cancel();
```

## サポートされない機能

### DelayedCall

`LeanDelayedCall()`に相当するメソッドはLitMotionには存在しません。詳細は[よくある質問](faq.md)を参照してください。

### LTSpline

`LTSpline`に相当する機能はサポートされません。ただし、同等の処理をLitMotionをUnityの[Splinesパッケージ](https://docs.unity3d.com/Packages/com.unity.splines@2.1/manual/index.html)と組み合わせて利用することで実現できます。
