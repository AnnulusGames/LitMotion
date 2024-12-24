# LitMotion v1から移行する

v2では、新機能の追加に加え一部のAPIの刷新が行われました。そのため、v1から移行するために変更が必要な部分がいくつかあります。

## モーションの再利用

v1では`MotionBuilder`に対して`Preserve()`メソッドが利用できましたが、これはv2では廃止されました。

代わりに`MotionHandle.Preserve()`が追加されました。`Preserve()`を呼び出した`MotionHandle`は完了後も無効にならないため、同じモーションを再利用することが可能になります。(ただし、明示的に`Cancel()`で破棄させる必要が生じます)

また、同じモーションの設定を使い回したい場合は`MotionSettings<T, TOptions>`が利用できます。

## Bind拡張メソッドの移動

`BindToUnityLogger()`と`BindToProgress()`は`LitMotion.Extensions`に移動されました。

これらの拡張メソッドを利用するにはLitMotion.Extensions.asmdefの参照と`using LitMotion.Extensions;`が必要になることに注意してください。

## `BindWithState()` -> `Bind()`

v1でクロージャを避けるために存在していた`BindWithState()`は`Bind()`のオーバーロードに変更されました。また、v2からは構造体のStateを受け入れるようになっています。(これは若干のアロケーションが発生しますが、アロケーションの少なさや呼び出し速度の点でクロージャよりも効率的です)

```cs
// v1
LMotion.Create(0f, 1f, 1f)
    .BindWithState(state, (x, state) => { });

// v2
LMotion.Create(0f, 1f, 1f)
    .Bind(state, (x, state) => { });
```

## `ToYieldInteraction()` -> `ToYieldInstruction()`

コルーチンで`MotionHandle`を待機するためのメソッド名が`ToYieldInteraction()`から`ToYieldInstruction()`に変更されます。(これはv1の命名が誤りであったため、v2で改めて修正されました)

## `LoopType.Yoyo`の挙動

v1の`LoopType.Yoyo`は命名から想定される動作になっていませんでしたが、v2ではこれが修正されました。従来の動作を維持したい場合は、v2で追加された`LoopType.Flip`を利用してください。

// TODO: 画像の追加

## enumの命名の変更

v2では一部のenumの命名がアメリカ英語の綴りに変更されます。

* `LinkBehaviour` -> `LinkBehavior`
* `CancelBehaviour` -> `CancelBehavior`

## `WithBindOnSchedule()` -> `WithImmediateBind()`

v1における`WithBindOnSchedule()`は`WithImmediateBind()`に名称が変更されました。また、デフォルトの値が`true`に変更されています。

```cs
// v1
LMotion.Create(0f, 1f, 1f)
    .WithBindOnSchedule()
    .BindToPositionX(transform);

// v2
LMotion.Create(0f, 1f, 1f)
    .WithImmediateBind() // デフォルトがtrueであるため、明示的にこれを呼び出す必要はありません。
    .BindToPositionX(transform);
```

## ManualMotionDispatcher

v2では`ManualMotionDispatcher`クラスが通常のクラスに変更されました。これにより複数のManualMotionDispatcherを作成可能になります。

v1から移行する際には、代替として`ManualMotionDispatcher.Default`が利用可能です。これはグローバルに利用可能な`ManualMotionDispatcher`のインスタンスです。

```cs
// v1
ManualMotionDispatcher.Update(0.1);

// v2
ManualMotionDispatcher.Default.Update(0.1);
```

また、v2では`dispatcher.Scheduler`からそのDispatcherにスケジュールする`IMotionScheduler`を取得できます。`MotionScheduler.Manual`は引き続き利用可能で、これは`ManualMotionDispatcher.Default.Scheduler`のエイリアスになります。

## MotionTrackerウィンドウの廃止

v1で存在していたMotionTrackerウィンドウは廃止されました。v2ではより強力な機能を備えた[LitMotion Debugger](litmotion-debugger.md)が追加されているため、そちらを利用してください。