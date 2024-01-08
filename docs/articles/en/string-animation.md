# String Animation

You can create a motion to animate strings using `LMotion.String.Create**Bytes()`.

```cs
TMP_Text text;
LMotion.String.Create128Bytes("", "<color=red>Zero</color> Allocation <i>Text</i> Tween! <b>Foooooo!!</b>", 5f)
    .WithRichText()
    .WithScrambleChars(ScrambleMode.Lowercase)
    .BindToText(text);
```

![gif-img1](../../images/rich-text-animation.gif)

### Binding Numeric Motion to Text

It's also possible to bind numeric motion to text. When using `TMP_Text` as the target, you can perform binding with zero allocation using `BindToText()`.

```cs
TMP_Text text;
LMotion.Create(0, 999, 2f)
    .BindToText(text);
```

Moreover, it's possible to set formatting by passing a format string. Below is a sample motion for displaying a float number with comma separation up to two decimal places.

```cs
TMP_Text text;
LMotion.Create(0f, 100000f, 2f)
    .BindToText(text, "{0:N2}");
```

![gif-img2](../../images/bind-number-to-text.gif)

However, motions including this formatting use `string.Format()` internally, leading to GC allocations. To avoid this, you'll need to introduce ZString into your project. For more details, refer to the section on [ZString](integration-zstring.md).