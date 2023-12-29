# Vibration Motion with Punch/Shake

You can create a vibration motion using `LMotion.Punch.Create()` or `LMotion.Shake.Create()`.

```cs
LMotion.Punch.Create(0f, 5f, 2f)
    .BindToPositionX(target1);

LMotion.Shake.Create(0f, 5f, 2f)
    .BindToPositionX(target2);
```

![gif-img](../../images/punch-shake-motion.gif)

When creating a vibration motion, specify the initial value (startValue) as the first argument and the strength of the vibration (strength) as the second argument. The vibration will fluctuate within the range of `startValue ± strength`. Please note that the value range for vibration motion differs from the usual motion.

The difference between `Punch` and `Shake` lies in the behavior of the vibration. With `Punch`, the vibration is regular, while `Shake` exhibits random movement.

These motions offer specific settings:

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

For further details, please refer to the [Motion Configuration](motion-configuration.md) section.