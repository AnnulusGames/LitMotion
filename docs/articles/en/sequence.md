# Sequence

Using a `Sequence`, you can combine multiple motions. This is useful when controlling complex animations.

To create a `Sequence`, you build it with `LSequence.Create()`, add motions, and then call `Schedule()`.

```cs
LSequence.Create()
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Insert(0f, LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Schedule();
```

The return value of `Schedule()` is a `MotionHandle`, so it can be treated the same as any other motion.

> [!WARNING]  
> You cannot add motions that are already playing or those with infinite loops to a sequence. (An exception will occur.)

## Append

`Append()` adds motions sequentially after the previous motion in the sequence.

```cs
// The x, y, z motions will be played in order
LSequence.Create()
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Schedule();
```

You can also add a delay between motions using `AppendInterval()`.

```cs
LSequence.Create()
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .AppendInterval(0.5f)
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .AppendInterval(0.5f)
    .Append(LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Schedule();
```

## Join

`Join()` adds a motion at the start of the last added motion in the sequence, making them play simultaneously.

```cs
// The x, y, and z motions will play simultaneously
LSequence.Create()
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Join(LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Schedule();
```

## Insert

`Insert()` inserts a motion at a specified position in the sequence.

```cs
// Insert the x, y, and z motions at specified positions
LSequence.Create()
    .Insert(0.1f, LMotion.Create(0f, 1f, 1f).BindToPositionX(transform))
    .Insert(0.2f, LMotion.Create(0f, 1f, 1f).BindToPositionY(transform))
    .Insert(0.3f, LMotion.Create(0f, 1f, 1f).BindToPositionZ(transform))
    .Schedule();
```
