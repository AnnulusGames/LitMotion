# Migrate from LeanTween

This page provides a simple guide for migrating from LeanTween to LitMotion.

## Position Tweens

```cs
var endValue = new Vector3(1f, 2f, 3f);
var duration = 1f;

// LeanTween
LeanTween.move(gameObject, endValue, duration);

// LitMotion
LMotion.Create(transform.position, endValue, duration)
    .BindToPosition(transform);
```

## Value Tweens

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

## Tween Settings

```cs
// LeanTween
descr.setRepeat(2)
    .setLoopPingPong()
    .setEase(LeanTweenType.easeOutQuad);

// LitMotion
builder.WithLoops(2, LoopType.Flip)
    .WithEase(Ease.OutQuad);
```

## Tween Control

```cs
// LeanTween
descr.pause();
descr.cancel();

// LitMotion
handle.PlaybackSpeed = 0f;
handle.Cancel();
```

## Unsupported Features

### DelayedCall

There is no equivalent of `LeanTween.DelayedCall()` in LitMotion. For more details, refer to the [FAQ](./faq.md).

### LTSpline

LitMotion does not support an equivalent to `LTSpline`. However, you can achieve similar functionality by combining LitMotion with Unity's [Splines](https://docs.unity3d.com/Packages/com.unity.splines@2.1/manual/index.html) package.
