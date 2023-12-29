# Punch/Shakeによる振動のモーション

`LMotion.Punch.Create()`または`LMotion.Shake.Create()`を用いることで振動のモーションを作成できます。

```cs
LMotion.Punch.Create(0f, 5f, 2f)
    .BindToPositionX(target1);

LMotion.Shake.Create(0f, 5f, 2f)
    .BindToPositionX(target2);
```

![gif-img](../../images/punch-shake-motion.gif)

振動のモーションを作成する場合、第1引数には初期値(startValue)を指定し、第2引数には振動の強さ(strength)を指定します。振動の値は`startValue ± strength`の範囲で変化します。通常のモーションとは値の範囲が異なるので注意してください。

`Punch`と`Shake`の違いは振動の挙動です。`Punch`の場合は規則的な振動になりますが、`Shake`はランダムな動きの振動になります。

また、これらのモーションには特殊な設定がいくつか用意されています。

```cs
LMotion.Punch.Create(0f, 5f, 2f)
    .WithFrequency(20)
    .WithDampingRatio(0f)
    .BindToPositionX(target1);

LMotion.Shake.Create(0f, 5f, 2f)
    .WithFrequency(20)
    .WithDampingRatio(0f)
    .WithRandomSeed(123)
    .BindToPositionX(target2);
```

詳細は[モーションの設定](motion-configuration.md)を参照してください。