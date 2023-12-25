# Playing in UnityEditor

You can make motions operate within editor by specifying `EditorMotionScheduler.Update` as the Scheduler.

```cs
using LitMotion;
using LitMotion.Editor;

LMotion.Create(0f, 10f, 2f)
    .WithScheduler(EditorMotionScheduler.Update)
    .Bind(x => Debug.Log(x));
```

When using this Scheduler, motions will be driven on `EditorApplication.update`.
