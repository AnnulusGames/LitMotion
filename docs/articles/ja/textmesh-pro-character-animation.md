# TextMesh Proの文字をアニメーションさせる

LitMotionではTextMesh Proのテキストや値をアニメーションさせる機能に加え、一つ一つの文字を個別にアニメーションさせる機能が用意されています。

![gif-img1](../../images/textmeshpro-character-motion.gif)

```cs
TMP_Text text;
for (int i = 0; i < text.textInfo.characterCount; i++)
{
    LMotion.Create(Color.white, Color.red, 1f)
        .WithDelay(i * 0.1f)
        .WithEase(Ease.OutQuad)
        .BindToTMPCharColor(text, i);
    
    LMotion.Punch.Create(Vector3.zero, Vector3.up * 30f, 1f)
        .WithDelay(i * 0.1f)
        .WithEase(Ease.OutQuad)
        .BindToTMPCharPosition(text, i);
}
```

LitMotionで操作している文字の情報は、モーションの再生中は文字を書き換えても維持されますが、再生後はメッシュの更新(テキストの書き換えや`ForceMeshUpdate()`の呼び出しなど)によって初期値に戻ります。