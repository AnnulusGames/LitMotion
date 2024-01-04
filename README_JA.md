# LitMotion

Lightning-fast and Zero Allocation Tween Library for Unity.

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/header.png" width="800">

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

[English README is here.](README.md)

## 概要

LitMotionはUnity向けのハイパフォーマンスなトゥイーンライブラリです。LitMotionにはTransformなどのコンポーネントや独自のフィールド・プロパティをアニメーションさせるための豊富な機能が含まれており、簡単にアニメーションを作成できます。

LitMotionはMagic Tweenに続いて私が作成した2つ目のトゥイーンライブラリです。LitMotionはMagic Tweenの実装で得た経験をもとに、必要十分な機能を厳選しつつ、最速で動作させることを念頭に置いて設計されました。トゥイーンの作成や駆動などあらゆるシチュエーションにおいて、他のトゥイーンライブラリと比較して2倍から20倍以上の圧倒的なパフォーマンスを発揮します。当然、トゥイーン作成時のアロケーションも一切ありません。

## ドキュメント

ドキュメントのフルバージョンは[こちら](https://annulusgames.github.io/LitMotion/)から確認できます。

## 特徴

- あらゆる値をコード一行でアニメーション可能
- 構造体ベースの設計でゼロアロケーションを達成
- DOTSを活用して最適化された極めてハイパフォーマンスな実装
- ランタイムとエディタの両方で動作
- イージングや繰り返しなど複雑な設定を適用可能
- コールバック/コルーチンによる完了の待機
- FixedStringとTextMeshProによるゼロアロケーションな文字列のアニメーション
- UniRxを利用したObservableへの変換
- UniTaskを利用したasync/await対応
- `IMotionOptions`と`IMotionAdapter`を用いた型の拡張

## セットアップ

### 要件

* Unity 2021.3 以上
* Burst 1.6.0 以上
* Collection 1.5.1 以上
* Mathematics 1.0.0 以上

### インストール

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力する

```
https://github.com/AnnulusGames/LitMotion.git?path=src/LitMotion/Assets/LitMotion
```

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記

```json
{
    "dependencies": {
        "com.annulusgames.lit-motion": "https://github.com/AnnulusGames/LitMotion.git?path=src/LitMotion/Assets/LitMotion"
    }
}
```

## スタートガイド

LitMotionを用いることでTransformやMaterialなどの値を簡単にアニメーションさせることができます。モーションを作成するには`LMotion.Create()`を使用します。

以下にコードのサンプルを示します。詳細についてはドキュメントを確認してください。

```cs
using System;
using System.Threading;
using UnityEngine;
using UniRx; // UniRx
using Cysharp.Threading.Tasks; // UniTask
using LitMotion;
using LitMotion.Extensions;
#if UNITY_EDITOR
using LitMotion.Editor;
#endif

public class Example : MonoBehaviour
{
    [SerializeField] Transform target1;
    [SerializeField] Transform target2;
    [SerializeField] TMP_Text tmpText;

    void Start()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 2f) // (0f, 0f, 0f)から(1f, 1f, 1f)まで2秒間で値をアニメーション
            .BindToPosition(target1); // target1.positionにバインドする

        LMotion.Create(0f, 10f, 2f) // 0fから10fまで2秒間でアニメーション
            .WithEase(Ease.OutQuad) // イージング関数を指定
            .WithLoops(2, LoopType.Yoyo) // ループ回数やループの形式を指定
            .WithDelay(0.2f) // 遅延を設定
            .BindToUnityLogger(); // Debug.unityLoggerにバインドし、更新時に値をConsoleに表示する

        var value = 0f;
        LMotion.Create(0f, 10f, 2f) // 0fから10fまで2秒間でアニメーション
            .WithScheduler(MotionScheduler.FixedUpdate) // 実行タイミングをSchedulerで指定
            .WithOnComplete(() => Debug.Log("Complete!")) // コールバックを設定
            .WithCancelOnError() // Bind内で例外が発生したらモーションをキャンセルする
            .Bind(x => value = x); // 任意の変数やフィールド、プロパティにバインド可能
            .AddTo(gameObject) // GameObjectが破棄された際にモーションをキャンセルする
        
        LMotion.String.Create128Bytes("", "<color=red>Zero</color> Allocation <i>Text</i> Tween! <b>Foooooo!!</b>", 5f)
            .WithRichText() // RichTextタグを有効化
            .WithScrambleChars(ScrambleMode.All) // 表示されていない部分をランダムな文字で埋める
            .BindToText(tmpText); // TMP_Textにバインド (stringを生成せずにゼロアロケーションでテキストを更新する)

        LMotion.Punch.Create(0f, 5f, 2f) // Punchモーション(規則的な減衰振動)を作成
            .WithFrequency(20) // 振動の回数を指定
            .WithDampingRatio(0f) // 減衰比を指定
            .BindToPositionX(target2); // transform.position.xにバインド

        // 作成したモーションの制御は`MotionHandle`構造体を介して行う
        var handle = LMotion.Create(0f, 1f, 2f).RunWithoutBinding();

        if (handle.IsActive()) // モーションが再生中の場合はtrueを返す
        {
            handle.Cancel(); // モーションをキャンセルする
            handle.Complete(); // モーションを完了する
        }
    }

    // コルーチンに対応
    IEnumerator CoroutineExample()
    {
        var handle = LMotion.Create(0f, 1f, 2f).BindToUnityLogger();
        yield return handle.ToYieldInteraction(); // コルーチンで完了を待機
    }

    // UniTaskを利用したasync/awaitに対応
    async UniTask AsyncAwaitExample(CancellationToken cancellationToken)
    {
        var handle = LMotion.Create(0f, 1f, 2f).BindToUnityLogger();
        await handle; // MotionHandleを直接await
        await handle.ToUniTask(cancellationToken); // ToUniTaskでCancellationTokenを渡してawait
    }

    // UniRxを利用したIObservable<T>への変換に対応
    void RxExample()
    {
        LMotion.Create(0f, 1f, 2f)
            .ToObservable() // モーションをIObservable<T>として作成
            .Where(x => x > 0.5f) // UniRxのオペレータを利用可能
            .Select(x => x.ToString())
            .Subscribe(x =>
            {
                tmpText.text = x;
            })
            .AddTo(this);
    }

#if UNITY_EDITOR

    // エディタ上での再生が可能
    void PlayOnEditor()
    {
        LMotion.Create(0f, 1f, 2f)
            .WithScheduler(EditorMotionScheduler.Update) // SchedulerにEditorMotionScheduler.Updateを指定
            .BindToUnityLogger();
    }
    
#endif
}
```

## パフォーマンス

ベンチマークの結果を以下に示します。ベンチマークのソースコードは[こちらのリポジトリ](https://github.com/AnnulusGames/TweenPerformance)から確認可能です。

### Tween 64,000 float properties

#### Startup

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_startup_64000_float.png" width="800">

#### Update

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_update_64000_float.png" width="800">

### Tween 50,000 transform.position

#### Startup

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_startup_50000_position.png" width="800">

#### Update

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_update_50000_position.png" width="800">

### GC Allocation (per position tween creation)

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_gc_position.png" width="800">

## サポート

Untiy forum: https://forum.unity.com/threads/litmotion-lightning-fast-and-zero-allocation-tween-library.1530427/

## ライセンス

[MIT License](LICENSE)