# カスタムAdapter

LitMotionでは拡張用のインターフェースとして`IMotionAdapter<T, TOptions>`と`IMotionOptions`が用意されています。

### Adapterの実装

`IMotionAdapter<T, TOptions>`を実装した構造体を定義することで独自の型をアニメーションできるようになります。以下は`Vector3`の対応を追加する`Vector3MotionAdapter`の実装例です。

```cs
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using LitMotion;

// JobをBurstに対応させるためにassembly属性が必要
// RegisterGenericJobType属性を追加し、MotionUpdateJob<T, TOptions, TAdapter>を登録する
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector3, NoOptions, Vector3MotionAdapter>))]

// IMotionAdapterを実装した構造体を作成する
// 型引数には対象の値の型と追加のオプション(必要ない場合はNoOptions)を指定する
public readonly struct Vector3MotionAdapter : IMotionAdapter<Vector3, NoOptions>
{
    // Evaluate内に値を補間する処理を実装する
    public Vector3 Evaluate(ref Vector3 startValue, ref Vector3 endValue, ref NoOptions options, in MotionEvaluationContext context)
    {
        return Vector3.LerpUnclamped(startValue, endValue, context.Progress);
    }
}
```

Adapterは状態を持つことができません。そのため余計なフィールドは定義しないでください。

また、LitMotionは内部でジェネリックJobを使用します。そのためユーザー定義の型を使用する際には`RegisterGenericJobType`属性をアセンブリに追加してBurstが型を認識できるようにする必要があります。

モーションに特別な状態を持たせたい場合には`IMotionOptions`を実装したunmanagedな構造体を定義します。以下はint型やlong型のモーションに使用される`IntegerOptions`の実装の一部です。

```cs
public struct IntegerOptions : IMotionOptions, IEquatable<IntegerOptions>
{
    public RoundingMode RoundingMode;

    // Equals, GetHashCode, etc.
    ...
}
```

オプションが必要ない場合には`NoOptions`型を指定します。

### カスタムAdapterを使用してモーションを作成する

`LMotion.Create`でMotionBuilderを作成する際に、型引数にAdapterの型を渡すことでカスタムAdapterを使用できます。

```cs
LMotion.Create<Vector3, NoOptions, Vector3MotionAdapter>(from, to, duration)
    .BindToPosition(transform);
```
