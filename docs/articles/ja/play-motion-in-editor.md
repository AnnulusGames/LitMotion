# Unity Editor上でモーションを再生する

LitMotionはエディタ上での再生に対応しています。Edit Mode時にモーションを作成した場合、自動でEditor上にスケジュールされます。この場合、モーションは`EditorApplication.update`上で駆動されます。

```cs
using UnityEngine;
using LitMotion;

if (Application.isPlaying)
{
    // Play Mode(Runtime)では通常通りUpdate上で駆動される
    LMotion.Create(0f, 10f, 2f)
        .Bind(x => Debug.Log(x));
}
else
{
    // Edit Modeで作成するとEditorApplication.update上で駆動される
    LMotion.Create(0f, 10f, 2f)
        .Bind(x => Debug.Log(x));
}
```

明示的にSchedulerに`EditorMotionScheduler.Update`を指定することも可能です。

```cs
using LitMotion;
using LitMotion.Editor;

LMotion.Create(0f, 10f, 2f)
    .WithScheduler(EditorMotionScheduler.Update)
    .Bind(x => Debug.Log(x));
```