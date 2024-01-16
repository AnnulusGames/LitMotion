# 例外処理

`MotionDispatcher.RegisterUnhandledExceptionHandler()`を使用することで、`Bind`や`WithOnComplete`内で発生したハンドリングされていない例外に対する処理を設定することができます。デフォルトでは`UnityEngine.Debug.LogException(ex)`でConsoleに例外を表示するように設定されています。

```cs
using UnityEngine;
using LitMotion;

// 現在設定されている例外処理(Action<Exception>)を取得する
var handler = MotionDispatcher.GetUnhandledExceptionHandler();

// LogExceptionではなくLogWarningで警告を表示するように変更する
MotionDispatcher.RegisterUnhandledExceptionHandler(ex => Debug.LogWarning(ex));
```
