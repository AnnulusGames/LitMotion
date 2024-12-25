# Binding

In LitMotion, it is required to bind the values to the target fields or properties when creating a motion. The values of the motion are updated within the PlayerLoop specified by the Scheduler, and the latest values are reflected in the bound fields/properties.

```cs
var value = 0f;
LMotion.Create(0f, 10f, 2f)
    .Bind(x => value = x); // Pass Action<T> to update the value
```

### Avoiding Allocations

Lambda expressions passed to `Bind()` cause allocations due to capturing external variables (known as closures).


Additionally, by passing the state as an argument to `Bind()`, you can avoid allocations caused by closures.

```cs
class FooClass
{
    public float Value { get; set; }
}

var target = new FooClass();

LMotion.Create(0f, 10f, 2f)
    .Bind(target, (x, target) => target.Value = x); // Pass the target object as the first argument
```

### Extension Methods

The `LitMotion.Extensions` namespace provides extension methods to simplify value binding.

```cs
using UnityEngine;
using LitMotion;
using LitMotion.Extensions; // Include this namespace

public class Example : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 2f)
            .BindToPosition(target); // Bind value to target.position

        LMotion.Create(1f, 3f, 2f)
            .BindToLocalScaleX(target); // Bind value to target.localScale.x
    }
}
```

### Playing Without Binding

To play motions without value binding, use `RunWithoutBinding()` to create a motion without binding.

```cs
LMotion.Create(0f, 0f, 2f)
    .WithOnComplete(() => Debug.Log("Complete!"))
    .RunWithoutBinding();
```

---
If you require further assistance or have more questions, feel free to ask!