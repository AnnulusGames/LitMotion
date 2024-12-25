# Sequence

Sequenceを用いることで、複数のモーションを合成できます。これは複雑なモーションを制御する際に便利です。

Sequenceを作成するには`LSequence.Create()`でBuilderを作成し、モーションの追加後に`Run()`を呼び出します。

```cs
LSequence.Create()
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Insert(0f, LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Run();
```

`Run()`の戻り値は`MotionHandle`であるため、通常のモーションと同じように扱うことが可能です。

> [!WARNING]
> Sequenceに再生中のモーションや無限ループするモーションを追加することはできません。(例外が発生します)

## Append

`Append()`はSequenceの後続にモーションを連結します。

```cs
// x, y, zのモーションが順番に再生される
LSequence.Create()
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Run();
```

`AppendInterval()`で待機時間を追加することも可能です。

```cs
LSequence.Create()
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .AppendInterval(0.5f)
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .AppendInterval(0.5f)
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Run();
```

## Join

`Join()`は直前にAppendで追加されたモーションの始点にモーションを追加します。

```cs
// x, y, zのモーションが同時に再生される
LSequence.Create()
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Run();
```

## Insert

`Insert()`は引数で指定された位置にモーションを挿入します。

```cs
// 指定の位置にx, y, zのモーションを挿入する
LSequence.Create()
    .Insert(0.1f, LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Insert(0.2f, LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Insert(0.3f, LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Run();
```
