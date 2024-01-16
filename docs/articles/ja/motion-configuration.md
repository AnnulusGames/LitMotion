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

#### WithOnComplete

再生終了時のコールバックを指定します。

#### WithOnCancel

キャンセル時のコールバックを指定します。

#### WithScheduler

モーションの再生に使用するSchedulerを指定します。作成したモーションは指定したSchedulerに対応したPlayerLoopで更新が行われます。

| Scheduler | 動作 |
| - | - |
| MotionScheduler.Initialization | Initializationのタイミングで更新を行います。 |
| MotionScheduler.InitializationIgnoreTimeScale | Initializationのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.InitializationRealtime | Initializationのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.EarlyUpdate | EarlyUpdateのタイミングで更新を行います。 |
| MotionScheduler.EarlyUpdateIgnoreTimeScale | EarlyUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.EarlyUpdateRealtime | EarlyUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.FixedUpdate | FixedUpdateのタイミングで更新を行います。 |
| MotionScheduler.FixedUpdateIgnoreTimeScale | FixedUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.FixedUpdateRealtime | FixedUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.PreUpdate | PreUpdateのタイミングで更新を行います。 |
| MotionScheduler.PreUpdateIgnoreTimeScale | PreUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.PreUpdateRealtime | PreUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.Update | Updateのタイミングで更新を行います。 |
| MotionScheduler.UpdateIgnoreTimeScale | Updateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.UpdateRealtime | Updateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.PreLateUpdate | PreLateUpdateのタイミングで更新を行います。 |
| MotionScheduler.PreLateUpdateIgnoreTimeScale | PreLateUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.PreLateUpdateRealtime | PreLateUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.PostLateUpdate | PostLateUpdateのタイミングで更新を行います。 |
| MotionScheduler.PostLateUpdateIgnoreTimeScale | PostLateUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.PostLateUpdateRealtime | PostLateUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.TimeUpdate | TimeUpdateのタイミングで更新を行います。 |
| MotionScheduler.TimeUpdateIgnoreTimeScale | TimeUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視します。 |
| MotionScheduler.TimeUpdateRealtime | TimeUpdateのタイミングで更新を行います。また、`Time.timeScale`の影響を無視し、`Time.realtimeSinceStartup`を用いて時間の計算を行います。 |
| MotionScheduler.Manual | 更新を手動で行います。詳細は[モーションを手動で更新する](updating-motion-manually.md)を参照してください。 |
| EditorMotionScheduler.Update (LitMotion.Editor) | EditorApplication.updateのタイミングで更新を行います。このSchedulerはエディタ限定で使用できます。 |

#### WithRoundingMode (int, long)

小数点以下の値の丸め方を設定します。このオプションはint型のモーションにのみ適用可能です。

| RoundingMode | 動作 |
| - | - |
| RoundingMode.ToEven | デフォルトの設定。最も近い整数値に値を丸め、値が中間にある場合は最も近い偶数に丸めます。 |
| RoundingMode.AwayFromZero | 一般的な四捨五入の動作。最も近い整数値に値を丸め、値が中間にある場合は0から遠ざかる方向に値を丸めます。 |
| RoundingMode.ToZero | 0に近づく方向に値を丸めます。 |
| RoundingMode.ToPositiveInfinity | 正の無限大に近づく方向に値を丸めます。 |
| RoundingMode.ToNegativeInfinity | 負の無限大に近づく方向に値を丸めます。 |

#### WithScrambleMode (FixedString-)

まだ表示されていない文字の部分をランダムな文字で埋めることができます。このオプションは文字列のモーションにのみ適用可能です。

| ScrambleMode | 動作 |
| - | - |
| ScrambleMode.None | デフォルトの設定。まだ表示されていない部分には何も表示されません。 |
| ScrambleMode.Uppercase | ランダムな大文字のアルファベットで空白を埋めます。 |
| ScrambleMode.Lowercase | ランダムな小文字のアルファベットで空白を埋めます。 |
| ScrambleMode.Numerals | ランダムな数字で空白を埋めます。 |
| ScrambleMode.All | ランダムな大文字/小文字のアルファベット、または数字で空白を埋めます。 |
| (ScrambleMode.Custom) | 指定された文字列の中のランダムな数字で空白を埋めます。このオプションは明示的に指定できず、WithScrambleModeの引数にstringを渡した際に設定されます。|

#### WithRichText (FixedString-)

RichTextのサポートを有効化し、RichTextタグが含まれるテキストの文字送りが可能になります。このオプションは文字列のモーションにのみ適用可能です。

#### WithFrequency (Punch, Shake)

Punch, Shakeの振動の周波数(終了時までの振動回数)を設定します。初期値は10に設定されています。

#### WithDampingRatio (Punch, Shake)

Punch, Shakeの振動の減衰比を設定します。この値が1の場合は完全に減衰し、0の場合は一切減衰しません。初期値は1に設定されています。

#### WithRandomSeed (FixedString-, Shake)

モーションの再生の際に使用する乱数のシードを指定できます。これによりScrambleCharsや振動のランダムな動作を制御することができます。