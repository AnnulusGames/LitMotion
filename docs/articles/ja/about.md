# LitMotionとは

![header](../../images/header.png)

LitMotionはUnity向けのハイパフォーマンスなトゥイーンライブラリです。LitMotionにはTransformなどのコンポーネントや独自のフィールド・プロパティをアニメーションさせるための豊富な機能が含まれており、簡単にアニメーションを作成できます。

LitMotionは[Magic Tween](https://github.com/AnnulusGames/MagicTween)に続いて私が作成した2つ目のトゥイーンライブラリです。LitMotionはMagic Tweenの実装で得た経験をもとに、必要十分な機能を厳選しつつ、最速で動作させることを念頭に置いて設計されました。トゥイーンの作成や駆動などあらゆるシチュエーションにおいて、他のトゥイーンライブラリと比較して2倍から20倍以上の圧倒的なパフォーマンスを発揮します。当然、トゥイーン作成時のアロケーションも一切ありません。

LitMotionは高いパフォーマンスだけでなく、トゥイーンライブラリとして実用に足るだけの豊富な機能を有しています。

* あらゆる値をコード一行でアニメーション可能
* 構造体ベースの設計でゼロアロケーションを達成
* DOTSを活用して最適化された極めてハイパフォーマンスな実装
* ランタイムとエディタの両方で動作
* イージングや繰り返しなど複雑な設定を適用可能
* コールバック/コルーチンによる完了の待機
* UniRxを利用したObservableへの変換
* UniTaskを利用したasync/await対応
* `IMotionOptions`と`IMotionAdapter`を用いた型の拡張

### パフォーマンス

LitMotionはモーションの作成/実行の両方においてMagic Tweenを含むあらゆるUnity向けのトゥイーンライブラリよりも高速に動作します。ベンチマークの結果を以下に示します。

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

### DOTween/Magic Tweenとの比較

他のUnity向けのトゥイーンライブラリとして、有名なDOTweenや先ほど挙げたMagic Tweenなどがあります。これらのライブラリと比較すると、LitMotionは以下のような特徴を持ちます。

* 良好なパフォーマンス
  - LitMotionはDOTweenと比較して5倍ほど高速に動作します。
  - より高速なMagic Tweenと比較しても1.5倍ほど高速です。
  - また、モーション作成時のアロケーションは一切ありません。
* 厳選された機能
  - LitMotionは必要十分な機能を持ちますが、MagicTweenやDOTweenと比較するとその機能は少なめです。これはこのライブラリが「Simpleであること」というコンセプトの元に作成されているためです。(もちろん拡張の余地は用意されています)
* シンプルで柔軟なAPI
  - メソッドチェーンを用いた自然な手触りで、モーションの作成からバインドまでをスムーズに書くことができます。
  - Magic TweenやDOTweenとは異なり、LitMotionはコンポーネントに対する拡張メソッドを含みません。拡張メソッドの利点は私も把握していますが、可読性の面で混乱を招くこともあります。LitMotionはAPIの簡潔さを優先しエントリーポイントを`LMotion`クラスに統一しています。
