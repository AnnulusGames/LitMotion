# クイックスタート

LitMotionを用いることでTransformやMaterialなどの値を簡単にアニメーションさせることができます。モーションを作成するには`LMotion.Create()`を使用します。

```cs
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

public class Example : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 2f) // (0f, 0f, 0f)から(1f, 1f, 1f)まで2秒間で値をアニメーション
            .BindToPosition(target); // target.positionにバインドする

        LMotion.Create(0f, 10f, 2f) // 0fから10fまで2秒間でアニメーション
            .BindToUnityLogger(); // Debug.unityLoggerにバインドし、更新時に値をConsoleに表示する

        var value = 0f;
        LMotion.Create(0f, 10f, 2f) // 0fから10fまで2秒間でアニメーション
            .Bind(x => value = x); // 任意の変数やフィールド、プロパティにバインド可能
    }
}
```

LitMotionのエントリーポイントは`LMotion.Create()`です。モーションを構築し、対象にバインドすることで値をアニメーションできます。