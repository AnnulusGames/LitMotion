# Design Philosophy

LitMotion is a tweening library based on a data-oriented approach, leveraging Unity's DOTS for optimization to achieve high performance. It prioritizes simplicity, making its overall design significantly more compact and intuitive compared to other tweening libraries.

### Utilization of DOTS

LitMotion is designed around DOTS, utilizing the C# Job System and Burst Compiler to achieve high performance (unlike Magic Tween, it does not require the Entities package).

To obtain the best performance using LitMotion, enabling Burst is necessary. For details, refer to the [official Burst package manual](https://docs.unity3d.com/ja/Packages/com.unity.burst@1.8/manual/index.html).

### Zero Allocation

While the introduction of [Incremental GC](https://docs.unity3d.com/Manual/performance-incremental-garbage-collection.html) has reduced the impact of garbage collection, unnecessary GC allocations can still cause CPU spikes and performance degradation. Spikes can be critical, particularly in games that demand precise actions, such as action or shooting games.

Motion in LitMotion is structured around structs, avoiding the instantiation of class instances during creation, resulting in complete elimination of allocations.

Moreover, to avoid runtime dynamic memory allocations, it's possible to expand arrays to store motions during initialization using `MotionDispatcher.EnsureStorageCapacity()`.

### Simple and Intellisense Friendly API

LitMotion adopts a somewhat unique API for a tweening library. This is to ensure high readability and a coding experience that is friendly to IntelliSense.

Unlike many common tweening libraries, LitMotion does not offer extension methods for components. The entry point is primarily the `LMotion` class, making it instantly clear where LitMotion is being used in the code.

When building motion in MotionBuilder, you can add fine-tuned settings using method chaining. The naming convention for MotionBuilder's methods follows a consistent pattern, using terms like `With-` for settings and `Bind-` for binding, ensuring good readability. Coupled with IntelliSense, this maintains a smooth coding experience.

```cs
LMotion.Create(0f, 10f, 2f)
    .WithEase(Ease.OutQuad)
    .WithScheduler(MotionScheduler.FixedUpdate)
    .BindToUnityLogger()
    .AddTo(gameObject);
```

### Integration with Coroutines, Rx, async/await

Unlike DOTween or Magic Tween, LitMotion doesn’t support the functionality of a Sequence. Instead, it provides conversion methods to Coroutine, Observable, and UniTask.

While Sequences are easy to use and convenient, they can make the code complex and affect readability when constructing intricate animations. Also, DOTween's Sequence has complex behaviors that may not be fully understood from the code alone.

Let's consider rewriting the animation using LitMotion and UniTask:

```cs
for (int i = 0; i < 2; i++)
{
    // Motions created can be awaited directly
    await LMotion.Create(0f, 4f, 2f).BindToPositionY(target);
    await LMotion.Create(0f, 2f, 2f).BindToPositionX(target);
    Debug.Log("Callback1");
}

await UniTask.WaitForSeconds(1f);
await LMotion.Create(target.position, Vector3.zero, 2f).BindToPosition(target);
Debug.Log("Callback2");
```

Using async/await allows procedural animation description. UniTask usage avoids unnecessary allocations, ensuring good performance. (It's also possible to achieve similar functionality using UniTask and DOTween.)

For combining multiple animations, Reactive Extensions (Rx) can be used. LitMotion supports UniRx, adding extension methods to create motions as observables.

```cs
// Create motions as observables
var x = LMotion.Create(-5f, 5f, 2f).ToObservable();
var y = LMotion.Create(0f, 3f, 2f).ToObservable();
var z = LMotion.Create(-1f, 1f, 2f).ToObservable();

// Combine x, y, z observables to update position
Observable.CombineLatest(x, y, z, (x, y, z) => new Vector3(x, y, z))
	.Subscribe(x => transform.position = x);
```

Though it might have slightly lower performance, using Rx provides powerful flexibility in constructing animations due to its expressive nature. Leveraging UniRx's rich operators allows for powerful composition, making Rx a potent alternative to Sequences.

In the current Unity/C# environment, with several excellent means to handle asynchronous processes, the functionality of Sequences isn't considered essential. To build complex animations, combining UniRx/UniTask as demonstrated above provides a viable alternative.
