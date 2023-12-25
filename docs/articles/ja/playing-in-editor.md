# Unity Editor上で再生する

Schedulerに`EditorMotionScheduler.Update`を指定することでエディタ上でモーションを動作させることが可能になります。

```cs
using LitMotion;
using LitMotion.Editor;

LMotion.Create(0f, 10f, 2f)
    .WithScheduler(EditorMotionScheduler.Update)
    .Bind(x => Debug.Log(x));
```

このSchedulerを指定する場合、モーションは`EditorApplication.update`上で駆動されます。