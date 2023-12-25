# 設計思想

LitMotionはデータ指向を基本としたトゥイーンライブラリであり、UnityのDOTSを活用した最適化によって高いパフォーマンスを獲得しています。LitMotionは「Simpleであること」を最も重視しており、全体の設計は他のトゥイーンライブラリと比べて遥かにコンパクトで直感的です。

### DOTSの活用

LitMotionはDOTSをベースとした設計になっており、C# Job SystemとBurst Compilerを使用して高いパフォーマンスを実現します。(Magic Tweenとは異なりEntitiesパッケージは必要ありません。)

そのため、LitMotionを使用して最良のパフォーマンスを得るにはBurstを有効化する必要があります。詳細は[Burstパッケージの公式マニュアル](https://docs.unity3d.com/ja/Packages/com.unity.burst@1.8/manual/index.html)を参照してください。

### ゼロアロケーション

[Incremental GC](https://docs.unity3d.com/jp/current/Manual/performance-incremental-garbage-collection.html)の導入によって以前よりもGCの影響は少なくなりましたが、依然として余計なGCアロケーションはCPUスパイクやパフォーマンスの低下を引き起こす原因になり得ます。特にアクションゲームやシューティングゲームなどの緻密な操作を要求するゲームにおいてスパイクの発生は致命的です。

LitMotionのモーションは構造体をベースに設計されており、作成時にクラスのインスタンスをnewすることはありません。そのためアロケーションは完全にゼロに抑えられています。

また、実行時の動的なメモリ確保を避けたい場合には、`MotionDispatcher.EnsureStorageCapacity()`を使用して初期化時にあらかじめモーションを保存する配列を拡張しておくことも可能です。

### シンプルで書き心地の良いAPI

LitMotionはトゥイーンライブラリとしてはやや特殊なAPIを採用しています。これは可読性の高さとIntelliSenseフレンドリーな書き心地の良さを実現するためです。

LitMotionは他の一般的なトゥイーンライブラリとは異なりコンポーネントに対する拡張メソッドを提供しません。エントリーポイントは基本的に`LMotion`クラスのみです。そのため、コードのどこでLitMotionが使用されているかが一目でわかるようになっています。

MotionBuilderでモーションを構築していく際には、メソッドチェーンを用いて細かい設定を追加することができます。MotionBuilderの各メソッドの命名は設定を表す`With-`とバインドを表す`Bind-`などの一定の規則で統一されており、可読性も良好です。またIntelliSenseと併用することで、流れるような書き心地を維持しながらコードを書いていくことができます。

```cs
LMotion.Create(0f, 10f, 2f)
    .WithEase(Ease.OutQuad)
    .WithScheduler(MotionScheduler.FixedUpdate)
    .BindToUnityLogger()
    .AddTo(gameObject);
```

### コルーチン、Rx、async/awaitとの統合

DOTweenやMagic Tweenとは異なり、LitMotionはSequenceの機能をサポートしません。それに代わる機能としてコルーチンやObservable、UniTaskへの変換メソッドを提供します。

Sequenceは簡単に使用できて便利な反面、複雑なアニメーションを構築しようとするとコードが複雑化し可読性を損ねることがあります。また、DOTweenのSequenceには「無限ループするTweenを追加できない」「一部の設定が無視される」など、コードだけでは把握しきれない複雑な仕様があります。

実際にコードを見てみましょう。以下はDOTweenのSequenceを用いてアニメーションを組み合わせる例です。

```cs
// 2回繰り返すSequenceを作成
var sequence1 = DOTween.Sequence();
sequence1.Append(target.DOMoveY(4f, 2f))
    .Append(target.DOMoveX(2f, 2f))
    .AppendCallback(() => Debug.Log("Callback1"))
    .SetLoops(2);

// 先ほど作成したSequenceをネストしたものを作成
var sequence2 = DOTween.Sequence();
sequence2.Append(sequence1)
    .AppendInterval(1f)
    .Append(target.DOMove(Vector3.zero, 2f))
    .AppendCallback(() => Debug.Log("Callback2"));
```

これをLitMotionとUniTaskを使用して書き直すと以下のようになります。

```cs
for (int i = 0; i < 2; i++)
{
    // 作成したモーションをそのままawait可能
    await LMotion.Create(0f, 4f, 2f).BindToPositionY(target);
    await LMotion.Create(0f, 2f, 2f).BindToPositionX(target);
    Debug.Log("Callback1");
}

await UniTask.WaitForSeconds(1f);
await LMotion.Create(target.position, Vector3.zero, 2f).BindToPosition(target);
Debug.Log("Callback2");
```

async/awaitの利用により手続き的にアニメーションを記述できます。UniTaskを使用するため余計なアロケーションは発生せず、パフォーマンスも良好です。(UniTaskとDOTweenを用いて同様の処理を記述することは可能です。)

複数のアニメーションを合成する手段としてReactive Extensions(Rx)を使用する方法もあります。LitMotionはUniRxをサポートしているため、UniRxを導入することでモーションをObservableとして作成する拡張メソッドが追加されます。

```cs
// モーションをObservableとして作成する
var x = LMotion.Create(-5f, 5f, 2f).ToObservable();
var y = LMotion.Create(0f, 3f, 2f).ToObservable();
var z = LMotion.Create(-1f, 1f, 2f).ToObservable();

// x, y, zのObservableを合成してpositionに反映する
Observable.CombineLatest(x, y, z, (x, y, z) => new Vector3(x, y, z))
	.Subscribe(x => transform.position = x);
```

パフォーマンスの面ではやや劣りますが、Rxの高い表現力を利用して柔軟にアニメーションを構築できます。UniRxの提供する豊富なオペレータを併用することが可能であるため、Sequenceの代替として使用する場合にはRxは非常に強力な手段となり得ます。

このように現在のUnity/C#の環境においては非同期処理を扱うための優れた手段がいくつも存在するため、Sequenceの機能は必須ではないと考えています。複雑なアニメーションを構築したい場合は、上のようにUniRx/UniTaskを併用して同様の機能を実現できます。
