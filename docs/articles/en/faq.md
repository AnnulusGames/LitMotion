# FAQ

## Is there a feature equivalent to `DelayedCall()`?

There is no method in LitMotion equivalent to `DelayedCall()`. This is by design.

In the modern era of async/await, delayed calls using callbacks should be avoided whenever possible. This is because exceptions are not propagated externally, making error handling more difficult. Instead, please use async methods.

However, if you are migrating from another tweening library and it is difficult to replace, you can use the following code as an alternative:

```cs
// The value doesn't matter, so any value will work
LMotion.Create(0f, 1f, delay)
    .WithOnComplete(action)
    .RunWithoutBinding();
```

## Can I add a callback to Sequence?

In DOTween, the Sequence has `AppendCallback()`, but LitMotion does not implement this method. This is for the same reason that `DelayedCall()` is not implemented.

LitMotion's Sequence is designed for "combining multiple motions," and it intentionally omits other features. This is to avoid code complexity. (For more details, refer to the [Design Philosophy](./design-philosophy.md) section.)
