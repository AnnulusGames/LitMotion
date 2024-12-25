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

上記のメソッド/プロパティはモーションが既に終了している、または`MotionHandle`が初期化されていない場合に例外をスローします。`MotionHandle`の指すモーションが存在しているかをチェックするには`IsActive()`を使用します。

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();

if (handle.IsActive()) handle.Complete();
```

また、モーションが存在していたら`Complate()` / `Cancel()`を呼び出したい場合、`TryComplate()` / `TryCancel()`を用いて簡潔に記述できます。

```cs
handle.TryComplete();
handle.TryCancel();
```

### モーションの再利用

デフォルトでは完了したモーションは自動的に破棄されるため、`MotionHandle`を再利用することはできません。

同じモーションを再利用したい場合は`Preserve()`を使用します。これを呼び出すことで、完了後にモーションが破棄されなくなります。

```cs
// Preserve()を呼び出す
handle.Preserve();

// 完了後も再利用できる
handle.Complete();
handle.Time = 0;
```

ただし、`Preserve()`を呼び出したモーションは明示的に`Cancel()`が呼ばれるまで動作し続けます。使用後は`Cancel()`を呼び出すか、`AddTo()`でGameObjectなどにライフタイムを紐づけてください。

### モーションの再生速度

`MotionHandle.PlaybackSpeed`プロパティを操作することで、モーションの再生速度を変更することができます。これを使用してモーションのスロー再生や逆再生、一時停止などを行うことができます。

```cs
var handle = LMotion.Create(0f, 10f, 2f).RunWithoutBinding();
handle.PlaybackSpeed = 2f;
```

### モーションを手動で制御する

`Time`プロパティを用いることで手動でモーションを制御することができます。

```cs
// モーションの経過時間を手動で設定する
handle.Time = 0.5;
```

ただし、`Time`を操作して完了させたモーションもデフォルトでは自動的に破棄されます。手動でモーションを制御する場合は`Preserve()`を用いると良いでしょう。

### モーションの情報を取得する

`MotionHandle`のプロパティからモーションのデータを取得できます。

```cs
// ループあたり長さ
var duration = handle.Duration;

// モーション全体の長さ
var totalDuration = handle.TotalDuration;

// 遅延時間
var delay = handle.Delay;

// ループ回数
var loops = handle.Loops;

// 完了したループの回数
var complatedLoops = handle.ComplatedLoops;
```

また、モーションが再生中かどうかを取得するには`IsPlaying()`を利用します。これは`IsActive()`と似ていますが、`IsActive()`がモーションが破棄されるまで常に`true`を返すのに対し、こちらは`Preserve()`を呼び出した後にモーションが完了すると`false`になります。

```cs
if (handle.IsPlaying())
{
    DoSomething();
}
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