# Installation

LitMotion.Animation is distributed independently from the core package. Let's install is in your project to get started using it.


### Requirements

* Unity 2021.3 or later
* LitMotion 2.0.0 or later

### Install via Package Manager (Recommended)

You can install LitMotion using the Package Manager.

1. Open Package Manager by navigating to Window > Package Manager.
2. Click on the "+" button and select "Add package from git URL."
3. Enter the following URL:

```text
https://github.com/yn01-dev/LitMotion.git?path=src/LitMotion/Assets/LitMotion.Animation
```

![img1](../../images/img-setup-1.png)

Alternatively, you can open the `Packages/manifest.json` file and add the following line within the `dependencies` block:

```json
{
    "dependencies": {
        "com.annulusgames.lit-motion.animation": "https://github.com/yn01-dev/LitMotion.git?path=src/LitMotion/Assets/LitMotion.Animation"
    }
}
```

### Install from unitypackage

It's also possible to install LitMotion from the provided unitypackage file.

1. Navigate to the Releases section and download the latest unitypackage file.
2. Open the downloaded file and import it into your project.
