# Exception Handling

You can use `MotionDispatcher.RegisterUnhandledExceptionHandler()` to set up handling for unhandled exceptions that occur within `Bind` or `WithOnComplete`. By default, it is configured to display exceptions in the Console using `UnityEngine.Debug.LogException(ex)`.

```cs
using UnityEngine;
using LitMotion;

// Retrieve the currently set exception handling (Action<Exception>)
var handler = MotionDispatcher.GetUnhandledExceptionHandler();

// Change to display a warning using LogWarning instead of LogException
MotionDispatcher.RegisterUnhandledExceptionHandler(ex => Debug.LogWarning(ex));
```