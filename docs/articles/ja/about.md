# LitMotionとは

![header](../../images/header.png)

LitMotionはUnity向けのハイパフォーマンスなトゥイーンライブラリです。LitMotionにはTransformなどのコンポーネントや独自のフィールド・プロパティをアニメーションさせるための豊富な機能が含まれており、簡単にアニメーションを作成できます。

LitMotionの最大の特徴は非常に優れたパフォーマンスです。Unityの最新のテクノロジー、DOTSの一部であるC# Job SystemおよびBurst Compilerを全面的に活用した設計となっており、トゥイーンの作成や駆動などあらゆるシチュエーションにおいて、他のトゥイーンライブラリと比較して2倍から20倍以上の圧倒的なパフォーマンスを発揮します。当然、トゥイーン作成時のアロケーションも一切ありません。

また、v2より複数のモーションを合成するためのSequenceと、Inspectorからトゥイーンアニメーションを作成可能なLitMotion.Animationパッケージが追加されました。これにより機能面においてもDOTween ProやPrimeTweenと同等または以上に強力なライブラリとなっています。

## 特徴

* あらゆる値をコード一行でアニメーション可能
* 構造体ベースの設計でゼロアロケーションを達成
* DOTSを活用して最適化された極めてハイパフォーマンスな実装
* ランタイムとエディタの両方で動作
* イージングや繰り返しなど複雑な設定を適用可能
* コールバック/コルーチンによる完了の待機
* ゼロアロケーションなテキストアニメーション
* TextMesh Pro / UI Toolkitに対応
* Punch、Shakeなどの特殊なモーション
* UniRx/R3を利用したObservableへの変換
* UniTaskを利用したasync/await対応
* `IMotionOptions`と`IMotionAdapter`を用いた型の拡張
* `SerializableMotionSettings<T, TOptions>`によるInspectorとの統合
* デバッグ用のAPIおよびLitMotion Debuggerウィンドウ
* Sequenceによるアニメーションの合成
* Inspectorから複雑なアニメーションを作成可能なLitMotion.Animationパッケージ

### パフォーマンス

LitMotionはモーションの作成/実行の両方においてあらゆるUnity向けのトゥイーンライブラリよりも高速に動作します。ベンチマークの結果を以下に示します。

![benchmark_1](../../images/benchmark_startup_64000_float.png)

---

![benchmark_2](../../images/benchmark_update_64000_float.png)

---

![benchmark_3](../../images/benchmark_startup_50000_position.png)

---

![benchmark_4](../../images/benchmark_update_50000_position.png)

---

![benchmark_5](../../images/benchmark_gc_position.png)

ベンチマークに使用したプロジェクト及びソースコードは[こちらのリポジトリ](https://github.com/AnnulusGames/TweenPerformance)にて確認できます。

