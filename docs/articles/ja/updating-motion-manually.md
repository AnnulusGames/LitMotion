# モーションを手動で更新する

Schedulerに`MotionScheduler.Manual`を指定することで、モーションの更新処理を手動で行うように設定することができます。

```cs
// SchedulerにMotionScheduler.Manualを指定
var handle = LMotion.Create(value, endValue, 2f)
    .WithScheduler(MotionScheduler.Manual)
    .BindToUnityLogger();
```

`MotionScheduler.Manual`を指定したモーションは`ManualMotionDispatcher.Update()`を用いて手動で更新処理を行う必要があります。

```cs
while (handle.IsActive())
{
    var deltaTime = 0.1f;
    // ManualMotionDispatcher.Update()で更新を行う
    ManualMotionDispatcher.Update(deltaTime);
}
```

またDomain Reloadをオフにしている場合、モーションの状態が初期化されないため予期しない動作を起こすことがあります。これを避けるためには、起動時に`ManualMotionDispatcher.Reset()`で明示的に初期化を行う必要があります。

```cs
void Awake()
{
    ManualMotionDispatcher.Reset();
}
```