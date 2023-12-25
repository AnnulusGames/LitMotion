# MotionBuilderを再利用する

MotionBuilderはモーションを作成した際に自動でバッファをプールに返却するため、同じBuilderから複数回モーション作成することができません。例えば、以下のコードは例外をスローします。

```cs
var builder = LMotion.Create(0f, 10f, 2f);
builder.BindToUnityLogger();
builder.BindToUnityLogger(); // InvalidOperationExceptionをスロー
```

同じBuilderを使用して複数回モーションを作成したい場合には`Preserve()`を呼び出して内部の値を保持する必要があります。

また、Preserveを呼び出したBuilderは自動でバッファをプールに返却しないため、明示的に`Dispose()`を呼び出してBuilderを適切に破棄する必要があります。(Dispose忘れを防ぐためにusing変数を使用することを推奨します。)

```cs
using var builder = LMotion.Create(0f, 10f, 2f).Preserve();
builder.BindToUnityLogger();
builder.BindToUnityLogger();
```
