![vrtk logo](https://raw.githubusercontent.com/thestonefox/VRTK/master/Assets/VRTK/Examples/Resources/Images/logos/vrtk-capsule-clear.png)
> ### VRTK - Virtual Reality Toolkit
> A productive VR Toolkit for rapidly building VR solutions in Unity3d.

[![Slack](http://sysdia2.co.uk/badge.svg)](http://invite.vrtk.io)
[![Waffle](https://img.shields.io/badge/waffle-tracker-blue.svg)](http://tracker.vrtk.io)

| Supported SDK | Download Link |
|---------------|---------------|
| SteamVR Unity Asset | [SteamVR Plugin] |
| Oculus Utilities Unity Package | [Oculus Utilities] |

## Documentation

The documentation for the project can be found within this
repository in [DOCUMENTATION.md] which includes the up to date
documentation for this GitHub repository.

Alternatively, the stable versions of the documentation can be viewed
online at [http://docs.vrtk.io](http://docs.vrtk.io).

## Getting Started

> *VRTK requires a supported VR SDK to be imported into your Unity3d Project.*

 * Clone this repository `git clone https://github.com/thestonefox/VRTK.git`.
 * Open `VRTK` within Unity3d.
 * Add the `VRTK_SDKManager` script to a GameObject in the scene.

<details><summary>**Instructions for using the SteamVR Unity3d asset**</summary>

 * Import the [SteamVR Plugin] from the Unity Asset Store.
 * Drag the `[CameraRig]` prefab from the SteamVR plugin into the
 scene.
 * Check that `Virtual Reality Supported` is ticked in the
 `Edit -> Project Settings -> Player` menu.
 * Ensure that `OpenVR` is added in the `Virtual Reality SDKs` list
 in the `Edit -> Project Settings -> Player` menu.
 * Select the GameObject with the `VRTK_SDKManager` script attached
 to it.
  * Select `Steam VR` for each of the SDK Choices.
  * Click the `Auto Populate Linked Objects` button to find the
  relevant Linked Objects.
 * Optionally, browse the `Examples` scenes for example usage of the
 scripts.

</details>

<details><summary>**Instructions for using the Oculus Utilities Unity3d package**</summary>

 * Download the [Oculus Utilities] from the Oculus developer website.
 * Import the `OculusUtilities.unitypackage` into the project.
 * Drag the `OVRCameraRig` prefab from the Oculus package into the
 scene.
 * Check that `Virtual Reality Supported` is ticked in the
 `Edit -> Project Settings -> Player` menu.
 * Ensure that `Oculus` is added in the `Virtual Reality SDKs` list
 in the `Edit -> Project Settings -> Player` menu.
 * Select the GameObject with the `VRTK_SDKManager` script attached
 to it.
  * Select `Oculus VR` for each of the SDK Choices.
  * Click the `Auto Populate Linked Objects` button to find the
  relevant Linked Objects.

</details>
   
## What's In The Box

VRTK is a collection of useful scripts and concepts to aid building VR
solutions rapidly and easily in Unity3d 5+.

It covers a number of common solutions such as:

 * Locomotion within virtual space.
 * Interactions like touching, grabbing and using objects
 * Interacting with Unity3d UI elements through pointers or touch.
 * Body physics within virtual space.
 * 2D and 3D controls like buttons, levers, doors, drawers, etc.
 * And much more...

VRTK is split into four main sections:

 * Prefabs - `VRTK/Prefabs/`
 * Scripts - `VRTK/Scripts/`
 * Examples - `VRTK/Examples/`
 * SDK - `VRTK/SDK`

The `VRTK` directory is where all of the relevant files are kept
and this directory can be simply copied over to an existing project.

*VRTK is heavily inspired by the [SteamVR Plugin for Unity3d Github Repo].*

## Examples

A collection of example scenes have been created to aid with
understanding the different aspects of VRTK.

A list of the examples can be viewed in [EXAMPLES.md] which includes
an up to date list of examples showcasing the features of VRTK.

The examples have all been built to work with the [SteamVR Plugin] by
default, but they can be converted over to using the [Oculus Utilities]
package by following the instructions for using the Oculus Utilities
package above.

> *If the examples are not working on first load, click the `[VRTK]`
> GameObject in the scene hierarchy to ensure the SDK Manager editor
> script successfully sets up the project and scene.*

## Made With VRTK

Many games and experiences have already been made with VRTK.

Check out the [Made With VRTK Document] to see the full list.

## Contributing

I would love to get contributions from you! Follow the instructions
below on how to make pull requests.

For the full contribution guidelines see the [Contribution Document].

## Pull requests

 1. [Fork] the project, clone your fork, and configure the remotes.
 2. Create a new topic branch (from `master`) to contain your feature,
 chore, or fix.
 3. Commit your changes in logical units.
 4. Make sure all the example scenes are still working.
 5. Push your topic branch up to your fork.
 6. [Open a Pull Request] with a clear title and description.

## License

Code released under the [MIT License].

[SteamVR Plugin]: https://www.assetstore.unity3d.com/en/#!/content/32647
[SteamVR Plugin for Unity3d Github Repo]: https://github.com/ValveSoftware/openvr/tree/master/unity_package/Assets/SteamVR
[Oculus Utilities]: https://developer3.oculus.com/downloads/game-engines/1.10.0/Oculus_Utilities_for_Unity_5/
[MIT License]: https://github.com/thestonefox/SteamVR_Unity_Toolkit/blob/master/LICENSE
[Contribution Document]: https://github.com/thestonefox/SteamVR_Unity_Toolkit/blob/master/CONTRIBUTING.md
[Made With VRTK Document]: https://github.com/thestonefox/SteamVR_Unity_Toolkit/blob/master/MADEWITHVRTK.md
[DOCUMENTATION.md]: https://github.com/thestonefox/SteamVR_Unity_Toolkit/blob/master/DOCUMENTATION.md
[EXAMPLES.md]: https://github.com/thestonefox/SteamVR_Unity_Toolkit/blob/master/EXAMPLES.md
[Fork]: http://help.github.com/fork-a-repo/
[Open a Pull Request]: https://help.github.com/articles/using-pull-requests/