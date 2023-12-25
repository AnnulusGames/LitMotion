# モーションの設定

モーションの作成時にイージングや繰り返し回数、コールバックなどの設定を追加することができます。
設定を追加するには`With-`メソッドを使用します。

記述する際にはメソッドチェーンを用いて複数の設定を同時に適用できます。

```cs
var value = 0f;
LMotion.Create(0f, 10f, 2f)
    .WithEase(Ease.OutQuad)
    .WithComplete(() => Debug.Log("Complate!"))
    .Bind(x => value = x);
```

### メソッド一覧

#### WithEase

モーションに適用するイージング関数を指定します。

#### WithDelay

モーションの開始を指定された秒数だけ遅延させます。

#### WithLoops

モーションを繰り返す回数を設定します。デフォルトでは1に設定されています。
-1を設定することで、停止されるまで無限に繰り返すモーションを作成することも可能です。

また、第2引数に`LoopType`を設定することで繰り返し時の挙動を設定することが可能です。

| LoopType | 動作 | 
| - | - | 
| LoopType.Restart | デフォルトの設定。ループ終了時に開始値にリセットされます。| 
| LoopType.Yoyo | 開始値と終了地を往復するように値をアニメーションさせます。| 
| LoopType.Increment | ループごとに値が増加します。 | 

#### WithIgnoreTimeScale

モーションが`Time.timeScale`の影響を無視するかどうかを指定します。

#### WithOnComplete

再生終了時のコールバックを指定します。

#### WithScheduler

モーションの再生に使用するSchedulerを指定します。

| Scheduler | 動作 |
| - | - |
| MotionScheduler.Update | Updateのタイミングで更新を行います。 |
| MotionScheduler.LateUpdate | LateUpdateのタイミングで更新を行います。 |
| MotionScheduler.FixedUpdate | FixedUpdateのタイミングで更新を行います。 |
| EditorMotionScheduler.Update (LitMotion.Editor) | EditorApplication.updateのタイミングで更新を行います。このSchedulerはエディタ限定で使用できます。 |

#### WithRoundingMode (int)

小数点以下の値の丸め方を設定します。このオプションはint型のモーションにのみ適用可能です。

| RoundingMode | 動作 |
| - | - |
| RoundingMode.ToEven | デフォルトの設定。最も近い整数値に値を丸め、値が中間にある場合は最も近い偶数に丸めます。 |
| RoundingMode.AwayFromZero | 一般的な四捨五入の動作。最も近い整数値に値を丸め、値が中間にある場合は0から遠ざかる方向に値を丸めます。 |
| RoundingMode.ToZero | 0に近づく方向に値を丸めます。 |
| RoundingMode.ToPositiveInfinity | 正の無限大に近づく方向に値を丸めます。 |
| RoundingMode.ToNegativeInfinity | 負の無限大に近づく方向に値を丸めます。 |
