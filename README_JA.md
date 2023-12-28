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