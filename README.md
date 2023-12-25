# LitMotion

Lightning-fast and Zero Allocation Tween Library for Unity.

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/header.png" width="800">

[![license](https://img.shields.io/badge/LICENSE-MIT-green.svg)](LICENSE)

[日本語版READMEはこちら](README_JA.md)

## Overview

LitMotion is a high-performance tweening library for Unity. It encompasses a rich set of features to animate components like Transform, along with custom fields and properties, allowing for easy creation of animations.

Following my creation of [Magic Tween](https://github.com/AnnulusGames/MagicTween), LitMotion stands as my second tweening library. Leveraging the experience gained from implementing Magic Tween, LitMotion has been designed prioritizing essential features while ensuring it operates at the highest speed possible. It outperforms other tweening libraries by a staggering 2 to 20 times in various situations such as tween creation and execution. Naturally, there are no allocations during tween creation.

## Documentation

The complete documentation for LitMotion can be accessed [here](https://annulusgames.github.io/LitMotion/).

## Features

- Animate anything in one line of code.
- Achieves zero allocations due to its struct-based design.
- Highly optimized using DOTS (Data-Oriented Technology Stack).
- Works in both runtime and editor.
- Apply complex settings like easing and looping.
- Wait for completion via callbacks/coroutines.
- Convert to Observables using UniRx.
- Supports async/await via UniTask.
- Extend types using `IMotionOptions` and `IMotionAdapter`.

## Setup

### Requirements

* Unity 2021.3 or later
* Burst 1.6.0 or later
* Collection 1.5.1 or later
* Mathematics 1.0.0 or later

### Installation

1. Open Package Manager from Window > Package Manager.
2. Click the "+" button > Add package from git URL.
3. Enter the following URL:

```
https://github.com/AnnulusGames/LitMotion.git?path=/LitMotion/src/LitMotion/Assets/LitMotion
```

Alternatively, open Packages/manifest.json and add the following to the dependencies block:

```json
{
    "dependencies": {
        "com.annulusgames.lit-motion": "https://github.com/AnnulusGames/LitMotion.git?path=/LitMotion/src/LitMotion/Assets/LitMotion"
    }
}
```

## Performance

Here are the benchmark results. The benchmark source code can be found in [this repository](https://github.com/AnnulusGames/TweenPerformance).

### Tween 64,000 float properties

#### Startup

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_startup_64000_float.png" width="800">

#### Update

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_update_64000_float.png" width="800">

### Tween 50,000 transform.position

#### Startup

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_startup_50000_position.png" width="800">

#### Update

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_update_50000_position.png" width="800">

### GC Allocation (per position tween creation)

<img src="https://github.com/AnnulusGames/LitMotion/blob/main/docs/images/benchmark_gc_position.png" width="800">

## License

[MIT License](LICENSE)

