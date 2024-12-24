# LitMotion Animationコンポーネント

![img](../../images/img-litmotion-animation-inspector.png)

LitMotion AnimationはLitMotionを用いたアニメーションを作成するためのコンポーネントです。

## Settings

#### Play On Awake

trueの場合、起動時に自動でアニメーションが再生されます。

#### Animation Mode

Componentsで設定されたアニメーションの再生方法を指定します。`Parallel`は全てのアニメーションを同時に再生します。`Sequential`はアニメーションを上から順番に再生します。

## Components

LitMotion Animationでは`Animation Component`を組み合わせることでアニメーションを作成します。

![img](../../images/img-litmotion-animation-component.png)

\[Add...\]ボタンを押すことで新たなAnimation Componentを追加できます。

主要なUnityのコンポーネントをアニメーションさせるためのAnimation Componentが組み込みで用意されているほか、独自のAnimation Componentを作成することも可能です。

## Debug

アニメーションをプレビュー再生するためのパネルです。\[Play\]ボタンを押すことでアニメーションを再生できます。

これはEdit Mode / Play Modeの両方で動作します。