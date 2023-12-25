## Custom Adapter

In LitMotion, `IMotionAdapter<T, TOptions>` and `IMotionOptions` interfaces are provided for extension purposes.

### Implementing an Adapter

Implementing a structure that conforms to `IMotionAdapter<T, TOptions>` allows you to animate custom types. Below is an example of implementing `Vector3MotionAdapter` to add support for `Vector3`:

```cs
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using LitMotion;

// The assembly attribute is necessary to support Burst for Jobs
// Add the RegisterGenericJobType attribute to register MotionUpdateJob<T, TOptions, TAdapter>
[assembly: RegisterGenericJobType(typeof(MotionUpdateJob<Vector3, NoOptions, Vector3MotionAdapter>))]

// Create a structure implementing IMotionAdapter with type arguments specifying the target value type and additional options (if required, else use NoOptions)
public readonly struct Vector3MotionAdapter : IMotionAdapter<Vector3, NoOptions>
{
    // Implement the interpolation logic within the Evaluate method
    public Vector3 Evaluate(in Vector3 startValue, in Vector3 endValue, in NoOptions options, in MotionEvaluationContext context)
    {
        return Vector3.LerpUnclamped(startValue, endValue, context.Progress);
    }
}
```

Adapters cannot hold state, so refrain from defining unnecessary fields within them.

LitMotion utilizes generic Jobs internally. Therefore, when using custom types, you need to add the `RegisterGenericJobType` attribute to the assembly to ensure Burst recognizes the types.

If you want motions to have special states, implement an unmanaged structure conforming to `IMotionOptions`. Here is a partial implementation example for `IntegerOptions` used in motions involving `int` or `long` types:

```cs
public struct IntegerOptions : IMotionOptions, IEquatable<IntegerOptions>
{
    public RoundingMode RoundingMode;

    // Implement Equals, GetHashCode, etc.
    ...
}
```

Specify `NoOptions` if options are not necessary.

### Creating Motions Using Custom Adapters

When creating a MotionBuilder with `LMotion.Create`, you can use a custom adapter by passing the adapter type as a type argument.

```cs
LMotion.Create<Vector3, NoOptions, Vector3MotionAdapter>(from, to, duration)
    .BindToPosition(transform);
```