# 文字列のアニメーション

`LMotion.String.Create**Bytes()`を用いることで文字列をアニメーションさせるモーションを作成できます。

```cs
TMP_Text text;
LMotion.String.Create128Bytes("", "<color=red>Zero</color> Allocation <i>Text</i> Tween! <b>Foooooo!!</b>", 5f)
    .WithRichText()
    .WithScrambleChars(ScrambleMode.Lowercase)
    .BindToText(text);
```

![gif-img](../../images/rich-text-animation.gif)

LitMotionは`Unity.Collection`名前空間内にある固定長の文字列型`FixedString-`を使用します。そのため文字列のモーションを作成する際にはアニメーションさせる文字列の長さにあった関数を選択する必要があります

また、文字列をアニメーションさせる際は`BindToText()`を使用してTextMeshProのテキストにバインドすることが推奨されます。これは`TMP_Text`の`SetText()`を用いてバインドするため、完全にゼロアロケーションで文字列をアニメーションさせることが可能になります！

文字列のモーション専用の設定については[モーションの設定](motion-configuration.md)を参照してください。