# Quick Start

Using LitMotion allows you to easily animate values such as Transform or Material. To create a motion, use `LMotion.Create()`.

```cs
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;

public class Example : MonoBehaviour
{
    [SerializeField] Transform target;

    void Start()
    {
        LMotion.Create(Vector3.zero, Vector3.one, 2f) // Animates values from (0f, 0f, 0f) to (1f, 1f, 1f) over 2 seconds
            .BindToPosition(target); // Binds to target.position

        LMotion.Create(0f, 10f, 2f) // Animates values from 0f to 10f over 2 seconds
            .BindToUnityLogger(); // Binds to Debug.unityLogger, displaying values in the Console on updates

        var value = 0f;
        LMotion.Create(0f, 10f, 2f) // Animates values from 0f to 10f over 2 seconds
            .Bind(x => value = x); // Can bind to any variable, field, or property
    }
}
```

The entry point for LitMotion is `LMotion.Create()`. You can construct motions and animate values by binding them to targets.
