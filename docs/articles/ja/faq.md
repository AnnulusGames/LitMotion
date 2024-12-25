# よくある質問

## DelayedCallに相当する機能はありませんか？

`DelayedCall(action)`に相当するメソッドはLitMotionには存在しません。これは意図的なものです。

async/awaitが存在する現代において、コールバックによる遅延呼び出しは極力利用すべきではありません。例外が外部に伝播されないため、エラーハンドリングが難しくなります。代わりにasyncメソッドを利用してください。

ただし、別のトゥイーンライブラリからの移行などで置き換えが難しい場合は、代わりに以下のコードを利用できます。

```cs
// 値は必要ないため適当で構いません
LMotion.Create(0f, 1f, delay)
    .WithOnComplete(action)
    .RunWithoutBinding();
```

## Sequenceにコールバックを追加できませんか？

DOTweenのSequenceには`AppendCallback()`がありますが、LitMotionはこのメソッドを実装していません。これは`DelayedCall()`が実装されていないのと同じ理由です。

LitMotionのSequenceは「複数のモーションを合成する」ための機能であり、それ以外の機能を意図的に削っています。これはコードの複雑化を避けるためです。(詳細は[設計思想]の項目を参照してください)
