# インストール

LitMotion.Animationはコアパッケージとは独立して配布されています。プロジェクトに追加して使用を開始しましょう。

### 要件

* Unity 2021.3 以上
* LitMotion 2.0.0 以上

### Package Manager経由でインストール (推奨)

Package Managerを利用してLitMotion.Animationをインストールできます。

1. Window > Package ManagerからPackage Managerを開く
2. 「+」ボタン > Add package from git URL
3. 以下のURLを入力

```text
https://github.com/yn01-dev/LitMotion.git?path=src/LitMotion/Assets/LitMotion.Animation
```

![img1](../../images/img-setup-1.png)

あるいはPackages/manifest.jsonを開き、dependenciesブロックに以下を追記します。

```json
{
    "dependencies": {
        "com.annulusgames.lit-motion.animation": "https://github.com/yn01-dev/LitMotion.git?path=src/LitMotion/Assets/LitMotion.Animation"
    }
}
```

### unitypackageからインストール

配布されているunitypackageファイルからLitMotion.Animationをインストールすることも可能です。

1. Releasesから最新のリリースに移動
2. unitypackageファイルをダウンロード
3. ファイルを開き、プロジェクトにインポートする
