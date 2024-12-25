# Migrate from DOTween

This page provides a simple guide for migrating from DOTween to LitMotion.

## Position Tweens

```cs
var endValue = new Vector3(1f, 2f, 3f);
var duration = 1f;

// DOTween
transform.DOMove(endValue, duration);

// LitMotion
LMotion.Create(transform.position, endValue, duration)
    .BindToPosition(transform);
```

## Value Tweens

```cs
// DOTween
var value = 0f;
DOTween.To(() => value, x => value = x, endValue, duration);

// LitMotion
LMotion.Create(value, endValue, duration)
    .Bind(x => value = x);
```

## From

```cs
// DOTween
transform.DOMoveX(endValue, duration).From(startValue);

// LitMotion
LMotion.Create(startValue, endValue, duration)
    .BindToPositionX(transform);
```

## Punch / Shake

```cs
// DOTween
transform.DOPunchPosition(...);
transform.DOShakePosition(...);

// LitMotion
LMotion.Punch.Create(...)
    .BindToPosition(transform);
LMotion.Shake.Create(...)
    .BindToPosition(transform);
```

## Tween Settings

```cs
// DOTween
tween.SetLoops(2, LoopType.Yoyo)
    .SetEase(Ease.OutQuad);

// LitMotion
builder.WithLoops(2, LoopType.Yoyo)
    .WithEase(Ease.OutQuad);
```

## Tween Control

```cs
// DOTween
tween.Pause();
tween.Complete();
tween.Kill();

// LitMotion
handle.PlaybackSpeed = 0f;
handle.Complete();
handle.Cancel();
```

## Sequence

```cs
// DOTween
DOTween.Sequence()
    .Append(...)
    .Join(...)
    .Insert(...);

// LitMotion
LSequence.Create()
    .Append(...)
    .Join(...)
    .Insert(...)
    .Run();
```

Adding callbacks to Sequences is not supported in LitMotion. This is intentional, as LitMotion encourages the use of async methods for complex animations that require callbacks. For more details, refer to the [Design Philosophy](./design-philosophy.md).

## Update Timing Changes

```cs
// DOTween
tween.SetUpdate(UpdateType.Fixed);

// LitMotion
builder.WithScheduler(MotionScheduler.FixedUpdate);
```

## Linking to GameObject

```cs
// DOTween
tween.SetLink(gameObject);

// LitMotion
handle.AddTo(gameObject);
```

## Coroutines, async/await

```cs
// DOTween
yield return tween.WaitForCompletion();
await tween.AsyncWaitForCompletion();

// LitMotion
yield return handle.ToYieldInstruction();
await handle;
```

## Safe Mode

To display exceptions within a tween as warnings, similar to DOTween's Safe Mode, you can configure LitMotion as follows:

```cs
// Log caught exceptions as warnings in the console
MotionDispatcher.RegisterUnhandledExceptionHandler(ex => Debug.LogWarning(ex));
```

## Unsupported Features

### DelayedCall

There is no direct equivalent of `DOTween.DelayedCall()` in LitMotion. For more details, refer to the [FAQ](./faq.md).

### SetSpeedBased

LitMotion does not support a `SetSpeedBased()` equivalent. You should calculate the required duration based on the distance between the start and end points.

### DoPath

There is no equivalent to `transform.DoPath()` in LitMotion. However, you can achieve similar functionality by combining LitMotion with Unity's [Splines](https://docs.unity3d.com/Packages/com.unity.splines@2.1/manual/index.html) package.
