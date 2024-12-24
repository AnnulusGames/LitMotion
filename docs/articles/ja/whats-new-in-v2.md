# v2の新機能

LitMotion v2では機能面で大幅な強化が行われました。ここではv2で追加された機能について紹介します。

v1からの移行については[LitMotion v1から移行する](./migrate-from-v1.md)を参照してください。

## LitMotion.Animationパッケージ

Inspectorからアニメーションを作成するための機能を提供するLitMotion.Animationパッケージが追加されました。これはLitMotion本体とは別のパッケージとして提供され、プロジェクトに導入することで、ビジュアルエディタからアニメーションを作成するLitMotion Animationコンポーネントが利用可能になります。

![img](../../images/img-litmotion-animation.gif)

詳細は[LitMotion.Animation](./litmotion-animation-about.md)の項目を参照してください。

## Sequence

複数のモーションを合成するSequenceの機能が追加されました。`LSequence.Create()`からBuilderを作成してモーションを追加できます。

```cs
LSequence.Create()
    .Append(LMotion.Create(-5f, 5f, 0.5f).BindToPositionX(target))
    .Append(LMotion.Create(0f, 5f, 0.5f).BindToPositionY(target))
    .Append(LMotion.Create(-2f, 2f, 1f).BindToPositionZ(target))
    .Schedule();
```

詳細は[Sequence](./sequence.md)の項目を参照してください。

## MotionHandle

`MotionHandle`に以下のプロパティ・メソッドが追加されました。

```cs
MotionHandle handle;

// methods
handle.Preserve();
handle.IsPlaying();
handle.TryComplete();
handle.TryCancel();

// readonly property
handle.Duration;
handle.TotalDuration;
handle.Loops;
handle.ComplatedLoops;
handle.Delay;

// property
handle.Time;
```

## WithOnLoopComplete

各ループの完了時に呼び出される新たなコールバックが追加されました。これは`WothOnLoopComplete()`を用いて追加できます。

```cs
LMotion.Create(...)
    .WithOnLoopComplete(complatedLoops => { })
    .Bind(x => { })
```

## LitMotion Debugger

v1のMotionTrackerウィンドウの代わりに、より強力な機能を備えたLitMotion Debuggerを追加しました。また、デバッグ用のAPIが追加されています。

![img](../../images/img-litmotion-debugger.png)

詳細は[LitMotion Debugger](./litmotion-debugger.md)を参照してください。