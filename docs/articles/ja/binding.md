# 値のバインディング

LitMotionでは、モーションの作成時に値を対象のフィールドやプロパティに紐付ける必要があります。モーションの値はScriptRunBehaviourUpdateの前に挿入されるPlayerLoopによって更新され、バインドしたフィールド/プロパティに最新の値が反映されます。

```cs
var value = 0f;
LMotion.Create(0f, 10f, 2f)
    .Bind(x => value = x); // Action<T>で値を反映させる処理を渡す
```

### アロケーションの回避

`Bind()`に渡すラムダ式は外部変数をキャプチャするためアロケーションが発生します。(これはクロージャと呼ばれます)

対象がclassの場合には`Bind()`の代わりに`BindWithState()`を利用することでクロージャによるアロケーションを回避できます。

```cs
class FooClass
{
    public float Value { get; set; }
}

var target = new FooClass();

LMotion.Create(0f, 10f, 2f)
    .BindWithState(target, (x, target) => target.Value = x); // 第一引数に対象のオブジェクトを渡す
```

### 拡張メソッド

`LitMotion.Extensions`名前空間内にはバインディングをより簡単に行うための拡張メソッドが用意されています。

```cs
using UnityEngine;
using LitMotion;
using LitMotion.Extensions; // usingを追加

public class Example : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 2f)
            .BindToPosition(target); // 値をtarget.positionにバインドする

        LMotion.Create(1f, 3f, 2f)
            .BindToLocalScaleX(target); // 値をtarget.localScale.xにバインドする
    }
}
```

### バインディングなしで再生する

値のバインディングをせずにモーションを再生したい場合には、`RunWithoutBinding()`を呼び出してバインディングなしのモーションを作成します。

```cs
LMotion.Create(0f, 0f, 2f)
    .WithOnComplete(() => Debug.Log("Complete!"))
    .RunWithoutBinding();
```
