# Playing in Unity Editor

LitMotion supports playback in the Unity Editor. When creating motions in Edit Mode, they are automatically scheduled in the Editor. In this case, the motions are driven on `EditorApplication.update`.

```cs
using UnityEngine;
using LitMotion;

if (Application.isPlaying)
{
    // In Play Mode (Runtime), it operates as usual on Update
    LMotion.Create(0f, 10f, 2f)
        .Bind(x => Debug.Log(x));
}
else
{
    // In Edit Mode, it operates on EditorApplication.update
    LMotion.Create(0f, 10f, 2f)
        .Bind(x => Debug.Log(x));
}
```

You can explicitly specify `EditorMotionScheduler.Update` to the scheduler as well.

```cs
using LitMotion;
using LitMotion.Editor;

LMotion.Create(0f, 10f, 2f)
    .WithScheduler(EditorMotionScheduler.Update)
    .Bind(x => Debug.Log(x));
```