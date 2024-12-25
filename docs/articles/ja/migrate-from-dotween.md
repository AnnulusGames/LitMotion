# DOTweenから移行する

このページでは、DOTweenからLitMotionへ移行するための簡易的な対応を示します。

## 位置のトゥイーン

```cs
var endValue = new Vector3(1f, 2f, 3f);
var duration = 1f;

// DOTween
transform.DOMove(endValue, duration);

// LitMotion
LMotion.Create(tranform.position, endValue, duration)
    .BindToPosition(transform);
```

## 値のトゥイーン

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
transform.DOMoveX(endValue, duration).From(startValue) 

// LitMotion
LMotion.Create(startValue, endValue, duration)
    .BindToPositionX(transform);
```

## Punch / Shake

```cs
// DOTween
transform.DOPunchPosition(...) 
transform.DOShakePosition(...) 

// LitMotion
LMotion.Punch.Create(...)
    .BindToPosition(transform);
LMotion.Shake.Create(...)
    .BindToPosition(transform);
```

## トゥイーンの設定

```cs
// DOTween
tween.SetLoops(2, LoopType.Yoyo)
    .SetEase(Ease.OutQuad);

// LitMotion
builder.WithLoops(2, LoopType.Yoyo)
    .WithEase(Ease.OutQuad);
```

## トゥイーンの制御

```cs
// DOTween
tween.Pause();
tween.Complete();
tween.Kill();

// LitMotion
handle.PlaybackSpeed = 0f;
handle.Complate();
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
    .Schedule();
```

Sequenceに対するコールバックの追加はLitMotionではサポートされていません。これは意図的なもので、LitMotionではコールバックを多用する複雑なアニメーションはasyncメソッドを利用すべきだと考えているためです。詳細は[設計思想](./design-philosophy.md)を参照してください。

## 更新タイミングの変更

```cs
// DOTween
tween.SetUpdate(UpdateType.Fixed);

// LitMotion
builder.WithScheduler(MotionScheduler.FixedUpdate);
```

## GameObjectとの紐付け

```cs
// DOTween
tween.SetLink(gameObject);

// LitMotion
handle.AddTo(gameObject);
```


## コルーチン、async/await

```cs
// DOTween
yield return tween.WaitForCompletion();
await tween.AsyncWaitForCompletion();

// LitMotion
yield return handle.ToYieldInstruction();
await handle;
```

## Safe Mode

DOTweenのSafe Modeのようにトゥイーン内での例外を警告として表示したい場合、LitMotionでは以下のように設定することができます。

```cs
// catchした例外を警告としてConsoleに表示する
MotionDispatcher.RegisterUnhandledExceptionHandler(ex => Debug.LogWarning(ex));
```

## サポートされない機能

### DelayedCall

`DOTween.DelayedCall()`に相当するメソッドはLitMotionには存在しません。詳細は[よくある質問](faq.md)を参照してください。

### SetSpeedBased

`SetSpeedBased()`に相当する機能はLitMotionには存在しません。始点・終点の距離から必要なdurationの値を計算してください。

### DoPath

`transform.DoPath()`に相当する機能はサポートされません。ただし、同等の処理をLitMotionをUnityの[Splinesパッケージ](https://docs.unity3d.com/Packages/com.unity.splines@2.1/manual/index.html)と組み合わせて利用することで実現できます。
