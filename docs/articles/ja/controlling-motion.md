# モーションの制御

`Bind()`や`RunWithoutBinding()`などのモーションを作成するメソッドは全て`MotionHandle`構造体を戻り値に持ちます。

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
```

この構造体を介してモーションの制御を行うことができます。

### モーションの完了/キャンセル

再生中のモーションを完了させるには`Complete()`を呼び出します。

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.Complete();
```

再生中のモーションをキャンセルするには`Cancel()`を呼び出します。

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.Cancel();
```

### モーションの存在チェック

上記のメソッドはモーションが既に終了している、または`MotionHandle`が初期化されていない場合に例外をスローします。`MotionHandle`の指すモーションが存在しているかをチェックするには`IsActive()`を使用します。

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();

if (handle.IsActive) handle.Complete();
```

### GameObjectにキャンセル処理を紐づける

`AddTo()`を使用してGameObjectが破棄された際に自動でモーションをキャンセルさせることができます。

```cs
LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(this.gameObject);
```

### 複数のMotionHandleを一括管理する

複数のMotionHandleをまとめるためのクラスとして`CompositeMotionHandle`クラスが用意されています。`AddTo()`でこれにMotionHandleを紐付けることも可能です。

```cs
var handles = new CompositeMotionHandle();

LMotion.Create(0f, 10f, 2f)
    .Bind(() => Debug.Log(x))
    .AddTo(handles);
```