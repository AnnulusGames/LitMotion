# コルーチンでモーションを待機する

モーションの完了をコルーチン内で待機することが可能です。コルーチンで待機するには`MotionHandle`の`ToYieldInteraction()`を呼び出します。

```cs
IEnumerator CoruntineExample()
{
    yield return LMotion.Create(0f, 10f, 2f).Bind(x => Debug.Log(x))
        .ToYieldInteraction();
}
```

ただしコルーチンはイテレータの言語的制約から戻り値を持つことができない、並列で待機ができないなどの機能的な問題を抱えています。

代替手段として、UniTaskを用いたasync/awaitでモーションの待機を行うことを推奨します。UniTaskとの統合については[UniTask](integration-unitask.md)を参照してください。
