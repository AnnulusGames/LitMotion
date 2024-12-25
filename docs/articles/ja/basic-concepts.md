# 基本的な概念

LitMotionのAPIは数種類のパーツで構成されています。LitMotionを利用する際にはこれらの概念を理解する必要があります。

### MotionBuilder

モーションを構築するために使われる構造体です。`LMotion.Create()`は全て`MotionBuilder`を返します。

```cs
var builder = LMotion.Create(0f, 10f, 3f);
```

MotionBuilderにはモーションを構成するためのメソッドが用意されています。これらはメソッドチェーンを用いて記述可能です。

```cs
LMotion.Create(0f, 10f, 3f)
    .WithEase(Ease.OutQuad)
    .WithDelay(2f)
    .WithLoops(4, LoopType.Yoyo);
```

LitMotionのモーションは基本的に何らかの値にバインディングして使用します。バインドを行うことで実際にモーションが生成され再生を開始します。

```cs
var value = 0f;
LMotion.Create(0f, 10f, 3f)
    .WithEase(Ease.OutQuad)
    .Bind(x => value = x);
```

詳細は[値のバインディング](binding.md)および[モーションの設定](motion-configuration.md)を参照してください。

### MotionHandle

`MotionHandle`は作成されたモーションを制御するための構造体です。MotionBuilderの`Bind()`などのメソッドは全てこれを戻り値に持ちます。

```cs
var handle = LMotion.Create(0f, 10f, 3f).Bind(x => value = x);
```

モーションの存在チェックや完了/キャンセルなどの処理はMotionHandleを介して行うことが可能です。

```cs
var handle = LMotion.Create(0f, 10f, 3f).Bind(x => value = x);

if (handle.IsActive())
{
    handle.Complete();
    handle.Cancel();
}
```

詳細は[モーションを制御する](motion-control.md)を参照してください。

### MotionScheduler

モーションの更新タイミングはMotionSchedulerによって決定されます。使用するSchedulerは`WithScheduler()`で設定できます。

```cs
LMotion.Create(0f, 10f, 2f)
    .WithScheduler(MotionScheduler.FixedUpdate)
    .Bind(() => Debug.Log(x));
```

詳細は[モーションの設定](motion-configuration.md)を参照してください。

### MotionAdapter

2つの値を補間する処理は`IMotionAdapter<T, TOptions>`を実装した構造体に記述されています。組み込みのAdapterは`LitMotion.Adapters`名前空間内に定義されています。

また、モーションに特殊なオプションを追加したい場合には`IMotionOptions`を実装した構造体を定義します。

カスタムAdapterの作成については[カスタムAdapter](custom-adapter.md)の項目を参照してください。