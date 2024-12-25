## Avoid Dynamic Memory Allocation

You can expand the capacity of the internal array that holds motions beforehand by calling `MotionDispatcher.EnsureStorageCapacity()`. By ensuring the maximum anticipated capacity, such as during the app's startup, you can mitigate runtime dynamic memory allocation.

```cs
MotionDispatcher.EnsureStorageCapacity<float, NoOptions, FloatMotionAdapter>(500);
MotionDispatcher.EnsureStorageCapacity<Vector3, NoOptions, Vector3MotionAdapter>(1000);
```

Since storage differs for each combination of value and options, you need to call `EnsureStorageCapacity()` for each respective type.
