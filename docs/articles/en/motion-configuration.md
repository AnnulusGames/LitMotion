# Motion Configuration

You can add settings such as easing, repeat count, callbacks, etc., when creating a motion. To add settings, use the `With-` methods.

When writing, you can apply multiple settings simultaneously using method chaining.

```cs
var value = 0f;
LMotion.Create(0f, 10f, 2f)
    .WithEase(Ease.OutQuad)
    .WithComplete(() => Debug.Log("Complete!"))
    .Bind(x => value = x);
```

### List of Methods

#### WithEase

Specifies the easing function to apply to the motion.

#### WithDelay

Delays the start of the motion by the specified number of seconds.

#### WithLoops

Sets the number of times the motion will repeat. It's set to 1 by default.
Setting it to -1 creates a motion that repeats infinitely until stopped.

Additionally, you can set the behavior during repetition by specifying `LoopType` as the second argument.

| LoopType | Behavior |
| - | - |
| LoopType.Restart | Default behavior. Resets to the start value at the end of each loop. |
| LoopType.Yoyo | Animates the value back and forth between start and end values. |
| LoopType.Increment | Value increases with each loop. |

#### WithIgnoreTimeScale

Specifies whether the motion ignores the influence of `Time.timeScale`.

#### WithOnComplete

Specifies the callback at the end of the playback.

#### WithScheduler

Specifies the Scheduler used for motion playback.

| Scheduler | Behavior |
| - | - |
| MotionScheduler.Update | Updates at the Update timing. |
| MotionScheduler.LateUpdate | Updates at the LateUpdate timing. |
| MotionScheduler.FixedUpdate | Updates at the FixedUpdate timing. |
| MotionScheduler.Manual | Updates manually. For details, see [Updating Motion Manually](updating-motion-manually.md).  |
| EditorMotionScheduler.Update (LitMotion.Editor) | Updates at the EditorApplication.update timing. This Scheduler is limited to the editor. |

#### WithRoundingMode (int)

Sets the rounding mode for decimal values. This option is only applicable to int-type motions.

| RoundingMode | Behavior |
| - | - |
| RoundingMode.ToEven | Default setting. Rounds the value to the nearest integer and, in case of a tie, rounds to the nearest even number. |
| RoundingMode.AwayFromZero | Typical rounding behavior. Rounds the value to the nearest integer and away from zero in case of a tie. |
| RoundingMode.ToZero | Rounds the value towards zero. |
| RoundingMode.ToPositiveInfinity | Rounds the value towards positive infinity. |
| RoundingMode.ToNegativeInfinity | Rounds the value towards negative infinity. |

#### WithScrambleMode (FixedString-)

You can fill the yet-to-be-displayed characters with random characters. This option is applicable only to string motions.

| ScrambleMode | Description |
| - | - |
| ScrambleMode.None | Default setting. Nothing is displayed in the yet-to-be-displayed parts. |
| ScrambleMode.Uppercase | Fills spaces with random uppercase alphabet characters. |
| ScrambleMode.Lowercase | Fills spaces with random lowercase alphabet characters. |
| ScrambleMode.Numerals | Fills spaces with random numeral characters. |
| ScrambleMode.All | Fills spaces with random uppercase/lowercase alphabet or numeral characters. |
| (ScrambleMode.Custom) | Fills spaces with random numeral characters from the specified string. This option cannot be explicitly specified and is set when passing a string argument to WithScrambleMode. |

#### WithRichText (FixedString-)

Enables RichText support, allowing character advancement in text containing RichText tags. This option is applicable only to string motions. 

#### WithFrequency (Punch, Shake)

Sets the frequency (number of oscillations until the end) for Punch and Shake vibrations. The default value is set to 10.

#### WithDampingRatio (Punch, Shake)

Sets the damping ratio for Punch and Shake vibrations. When this value is 1, it fully dampens, and when it's 0, there is no damping at all. The default value is set to 1.

#### WithRandomSeed (FixedString-, Shake)

Allows you to specify a random seed used during motion playback. This controls the random behavior of ScrambleChars or vibrations. 