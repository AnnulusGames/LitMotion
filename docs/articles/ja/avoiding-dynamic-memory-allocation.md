# 動的なメモリ確保を回避する

`MotionDispatcher.EnsureStorageCapacity()`を呼び出すことで事前にモーションを保持する内部配列の容量を拡大しておくことができます。アプリの起動時などに想定される最大数の容量を確保しておくことで、実行時の動的なメモリ確保を抑えることできます。

```cs
MotionDispatcher.EnsureStorageCapacity<float, NoOptions>(500);
MotionDispatcher.EnsureStorageCapacity<Vector3, NoOptions>(1000);
```

ストレージは値とオプションの組み合わせごとに異なるため、それぞれの型で`EnsureStorageCapacity()`を呼び出す必要があります。