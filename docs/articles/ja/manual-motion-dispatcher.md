# ManualMotionDispatcher

`ManualMotionDispatcher`を用いることで、複数のモーションを手動で更新することが可能になります。

```cs
var dispatcher = new ManualMotionDispatcher();
```

Schedulerに`dispatcher.Scheduler`を指定することで、モーションの更新処理を作成したdispatcherで行うように設定することができます。

```cs
// Schedulerに作成したManualMotionDispatcherのSchedulerを指定
var handle = LMotion.Create(value, endValue, 2f)
    .WithScheduler(dispatcher.Scheduler)
    .BindToUnityLogger();
```

`dispatcher.Update(double deltaTime)`を用いて更新処理を行うことができます。

```cs
dispatcher.Update(0.1);
```

## ManualMotionDispatcher.Default

グローバルに利用可能な`ManualMotionDispatcher`が必要な場合は`ManualMotionDispatcher.Default`が利用できます。

```cs
ManualMotionDispatcher.Default.Update(0.1);
```

`MotionScheduler.Manual`は`ManualMotionDispatcher.Default.Scheduler`と同じです。

> [!WARNING]
> `ManualMotionDispatcher.Default`を利用する場合、Domain Reloadをオフに設定するとモーションの状態が初期化されないため予期しない動作を起こすことがあります。これを避けるためには、起動時に`Reset()`で明示的に初期化を行います。
> 
> ```cs
> void Awake()
> {
>     ManualMotionDispatcher.Default.Reset();
> }
> ```

