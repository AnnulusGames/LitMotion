# String Animation

You can create a motion to animate strings using `LMotion.String.Create**Bytes()`.

```cs
TMP_Text text;
LMotion.String.Create128Bytes("", "<color=red>Zero</color> Allocation <i>Text</i> Tween! <b>Foooooo!!</b>", 5f)
    .WithRichText()
    .WithScrambleChars(ScrambleMode.Lowercase)
    .BindToText(text);
```

![gif-img](../../images/rich-text-animation.gif)

LitMotion uses the `FixedString-` type within the `Unity.Collection` namespace for string motion. Therefore, when creating string motions, you need to select a function corresponding to the length of the string you wish to animate.

When animating strings, it's recommended to use `BindToText()` to bind to TextMeshPro text. This method uses `SetText()` from `TMP_Text`, enabling animation of strings with complete zero allocation!

For specific settings dedicated to string motion, refer to [Motion Configuration](motion-configuration.md).
