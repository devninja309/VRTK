# SteamVR Unity Toolkit

A collection of useful scripts and prefabs for building SteamVR titles
in Unity 5

  > #### Note:
  > This is very early alpha and does not offer much functionality at
  > present. I'm open to suggestions, ideas and bug finding/fixing.
  > Also, expect builds to break older versions as things are changing
  > fast at this stage, it will settle down when the project reaches
  > a beta stage.

## Quick Start

  * Clone this repository `git clone https://github.com/thestonefox/SteamVR_Unity_Toolkit.git`
  * Open the `SteamVR_Unity_Toolkit` within Unity3d
  * Browse the `Examples` scenes for example usage of the scripts

## Summary

This toolkit provides many common VR functionality within Unity3d such
as (but not limited to):

  * Controller button events with common aliases
  * Controller world pointers (e.g. laser pointers)
  * Player teleportation
  * Grabbing/holding objects using the controllers
  * Interacting with objects using the controllers

The toolkit is heavily inspired and based upon the
[SteamVR plugin for Unity3d v1.0.8](https://github.com/ValveSoftware/openvr/tree/master/unity_package/Assets/SteamVR).

The reason this toolkit exists is because I found the SteamVR plugin
to contain confusing to use or broken code and I decided to build a
collection of scripts/assets that I would find useful when building for
VR within Unity3d.

## What's In The Box

This toolkit project is split into two main sections:

  * SteamVR_Unity_Toolkit - `SteamVR_Unity_Toolkit/`
    * Prefabs - `SteamVR_Unity_Toolkit/Prefabs/`
    * Scripts - `SteamVR_Unity_Toolkit/Scripts/`
    * Required Includes - `SteamVR_Unity_Toolkit/Required Includes/`
  * Examples - `Examples/`

The `SteamVR_Unity_Toolkit` directory is where all of the relevant
files are kept and this directory can be simply copied over to an
existing project. The `Examples` directory contains useful scenes
showing the `SteamVR_Unity_Toolkit` in action.

### Prefabs

At present there is only one Prefab included which is the `[CameraRig]`
and it has been taken directly from the SteamVR Unity plugin example:
`Extras/SteamVR_TestThrow` scene as it includes the relevant `Model`
children on the controller (which seem to be missing from the default
prefab in the SteamVR plugin `Prefabs/[CameraRig].prefab`.

The `SteamVR_Unity_Toolkit/Prefabs/[CameraRig]` can be dropped into
any scene to provide instant access to a VR game camera via the VR
headset and tracking of the VR controllers including model
representations.

### Scripts

This directory contains all of the toolkit scripts that add VR
functionality to Unity.

The current available scripts are:

#### Controller Events (SteamVR_ControllerEvents)

The controller events script is attached to a Controller object within
the `[CameraRig]` prefab and provides event listeners for every button
press on the controller (excluding the System Menu button as this
cannot be overriden and is always used by Steam).

When a controller button is pressed, the script emits an event to
denote that the button has been pressed which allows other scripts
to listen for this event without needing to implement any controller
logic.

The script also has a public boolean pressed state for the buttons to
allow the script to be queried by other scripts to check if a button is
being held down.

When a controller button is released, the script also emits an event
denoting that the button has been released.

The controller touchpad has two states, it can either be `touched`
where the user simply presses their finger on the pressure sensitive
pad or it can be `clicked` where the user presses down on the pad until
it makes a clicking sound.

The Controller Events script deals with both touchpad touch and click
events separately.

There are two button axis on the controller:

  * Touchpad touch position, which has an x and y value depending
  on where the touchpad is currently being touched.
  * Trigger button, which has an x value depending on how much the
  trigger button is being depressed.

There are two additional events emitted when either the Touchpad axis
or the Trigger axis change their value which can be used to determine
the change in either of the axis for finer control such as using
the Touchpad to move a character, or knowing the pressure that the
trigger is being pressed.

The Touchpad Axis is reported via the `TouchpadAxis` payload variable
which is updated on any Controller Event.

The Trigger Axis is reported via the `buttonPressure` payload variable
which is updated on any Controller Event. Any other button press will
report a button pressure of 1 or 0 as all other buttons are digital
(they are either clicked or not clicked) but because the Trigger is
analog it will report a varying button pressure.

The amount of fidelity in the changes on the axis can be
determined by the `axisFidelity` parameter on the script, which is
defaulted to 1. Any number higher than 2 will probably give too
sensitive results.

The event payload that is emitted contains:

  * **controllerIndex:** The index of the controller that was used.
  * **buttonPressure:** A float between 0f and 1f of the amount of.
  pressure being applied to the button pressed.
  * **touchpadAxis:** A Vector2 of the position the touchpad is
  touched at.

There are also common action aliases that are emitted when controller
buttons are pressed. These action aliases can be mapped to a
preferred controller button. The aliases are:

  * **Toggle Pointer:** Common action of turning a laser pointer on/off
  * **Toggle Grab:** Common action of grabbing game objects
  * **Toggle Use:** Common action of using game objects
  * **Toggle Menu:** Common action of bringing up an in-game menu

Each of the above aliases can have the preferred controller button
mapped to their usage by selecting it from the drop down on the script
parameters window.

When the set button is pressed it will emit the actual button event as
well as an additional event that the alias is "On". When the set button
is released it will emit the actual button event as well as an
additional event that the alias button is "Off".

Listening for these alias events rather than the actual button events
means it's easier to customise the controller buttons to the actions
they should perform.

An example of the `SteamVR_ControllerEvents` script can be viewed in
the scene `Examples/002_Controller_Events` and code examples
of how the events are utilised and listened to can be viewed in the
script `Examples/Scripts/SteamVR_ControllerEvents_ListenerExample.cs`

#### Simple Laser Pointer (SteamVR_SimplePointer)

The Simple Pointer emits a coloured beam from the end of the controller
to simulate a laser beam. It can be useful for pointing to objects
within a scene and it can also determine the object it is pointing at
and the distance the object is from the controller the beam is being
emitted from.

The laser beam is activated by default by pressing the `Grip` on the
controller. The event it is listening for is the `AliasPointer` events
so the pointer toggle button can be set by changing the
`Pointer Toggle` button on the `SteamVR_ControllerEvents` script
parameters.

The Simple Pointer script is attached to a Controller object within the
`[CameraRig]` prefab and the Controller object also requires the
`SteamVR_ControllerEvents` script to be attached as it uses this for
listening to the controller button events for enabling and disabling
the beam.

The following script parameters are available:

  * **Pointer Hit Color:** The colour of the beam when it is colliding
  with a valid target. It can be set to a different colour for each
  controller.
  * **Pointer Miss Color:** The colour of the beam when it is not
  hitting a valid target. It can be set to a different colour for each
  controller.
  * **Show Play Area Cursor:** If this is enabled then the play area
  boundaries are displayed at the tip of the pointer beam in the
  current pointer colour.
  * **Handle Play Area Cursor Collisions:** If this is ticked then if
  the play area cursor is colliding with any other object then the
  pointer colour will change to the `Pointer Miss Color` and the
  `WorldPointerDestinationSet` event will not be triggered, which will
  prevent teleporting into areas where the play area will collide.
  * **Pointer Facing Axis:** The facing axis can also be set to match
  the direction the `[CameraRig`] Prefab is facing as if it is rotated
  then the beam will emit out of the controller at the wrong angle, so
  this setting can be adjusted to ensure the beam always projects
  forward.
  * **Pointer Thickness:** The thickness and length of the beam can
  also be set on the script as well as the ability to toggle the sphere
  beam tip that is displayed at the end of the beam (to represent a
  cursor).
  * **Pointer Length:** The distance the beam will project before
  stopping.
  * **Show Pointer Tip:** Toggle whether the cursor is shown on the end
  of the pointer beam.

The Simple Pointer object extends the `SteamVR_WorldPointer` abstract
class and therefore emits the same events and payload.

An example of the `SteamVR_SimplePointer` script can be viewed in
the scene `Examples/003_Controller_SimplePointer` and
code examples of how the events are utilised and listened to can be
viewed in the script
`Examples/Scripts/SteamVR_ControllerPointerEvents_ListenerExample.cs`

#### Bezier Curve Laser Pointer (SteamVR_BezierPointer)

The Bezier Pointer emits a curved line (made out of spheres) from the
end of the controller to a point on a ground surface (at any height).
It is more useful than the Simple Laser Pointer for traversing objects
of various heights as the end point can be curved on top of objects
that are not visible to the player.

The laser beam is activated by default by pressing the `Grip` on the
controller. The event it is listening for is the `AliasPointer` events
so the pointer toggle button can be set by changing the
`Pointer Toggle` button on the `SteamVR_ControllerEvents` script
parameters.

The Bezier Pointer script is attached to a Controller object within the
`[CameraRig]` prefab and the Controller object also requires the
`SteamVR_ControllerEvents` script to be attached as it uses this for
listening to the controller button events for enabling and disabling
the beam.

The following script parameters are available:

  * **Pointer Hit Color:** The colour of the beam when it is colliding
  with a valid target. It can be set to a different colour for each
  controller.
  * **Pointer Miss Color:** The colour of the beam when it is not
  hitting a valid target. It can be set to a different colour for each
  controller.
  * **Show Play Area Cursor:** If this is enabled then the play area
  boundaries are displayed at the tip of the pointer beam in the
  current pointer colour.
  * **Handle Play Area Cursor Collisions:** If this is ticked then if
  the play area cursor is colliding with any other object then the
  pointer colour will change to the `Pointer Miss Color` and the
  `WorldPointerDestinationSet` event will not be triggered, which will
  prevent teleporting into areas where the play area will collide.
  * **Pointer Facing Axis:** The facing axis can also be set to match
  the direction the `[CameraRig`] Prefab is facing as if it is rotated
  then the beam will emit out of the controller at the wrong angle, so
  this setting can be adjusted to ensure the beam always projects
  forward.
  * **Pointer Length:** The length of the projected forward pointer
  beam, this is basically the distance able to point from the
  controller potiion.
  * **Pointer Density:** The number of spheres to render in the beam
  bezier curve. A high number here will most likely have a negative
  impact of game performance due to large number of rendered objects.
  * **Show Pointer Cursor:** A cursor is displayed on the ground at
  the location the beam ends at, it is useful to see what height the
  beam end location is, however it can be turned off by toggling this.
  * **Pointer Cursor Radius:** The size of the ground pointer cursor,
  This number also affects the size of the spheres in the bezier curve
  beam. The larger the raduis, the larger the spheres will be.
  * **Pointer Facing Axis:** The facing axis can also be set to match the
  direction the `[CameraRig`] Prefab is facing as if it is rotated then
  the beam will emit out of the controller at the wrong angle, so this
  setting can be adjusted to ensure the beam always projects forward.

The Bezier Pointer object extends the `SteamVR_WorldPointer` abstract
class and therefore emits the same events and payload.

An example of the `SteamVR_BezierPointer` script can be viewed in
the scene `Examples/009_Controller_BezierPointer` which is used in
conjunction with the Height Adjust Teleporter shows how it is
possible to traverse different height objects using the curved
pointer without needing to see the top of the object.

Another example can be viewed in the scene
`Examples/012_Controller_PointerWithAreaCollision` that shows how
a Bezier Pointer with the Play Area Cursor and Collision Detection
enabled can be used to traverse a game area but not allow teleporting
into areas where the walls or other objects would fall into the play
area space enabling the player to enter walls.

The bezier curve generation code is in another script located at
`SteamVR_Unity_Toolkit/Scripts/Helper/CurveGenerator.cs` and was
heavily inspired by the tutorial and code from [Catlike Coding](http://catlikecoding.com/unity/tutorials/curves-and-splines/)

#### Basic Teleporter (SteamVR_BasicTeleport)

The basic teleporter updates the `[CameraRig]` x/z position in the
game world to the position of a World Pointer's tip location which is
set via the `WorldPointerDestinationSet` event. The y position is never
altered so the basic teleporter cannot be used to move up and down
game objects as it only allows for travel across a flat plane.

The Basic Teleport script is attached to the `[CameraRig]` prefab and
requires an implementation of the WorldPointer script to be attached
to another game object (e.g. SteamVR_SimplePointer attached to
the Controller object).

The following script parameters are available:

  * **Blink Transition Speed:** The fade blink speed can be changed on
  the basic teleport script to provide a customised teleport experience.
  Setting the speed to 0 will mean no fade blink effect is present.
  The fade is achieved via the `SteamVR_Fade.cs` script in the
  SteamVR Unity Plugin scripts.
  * **Headset Position Compensation:** If this is checked then the
  teleported location will be the position of the headset within the
  play area. If it is unchecked then the teleported location will
  always be the centre of the play area even if the headset position
  is not in the centre of the play area.

An example of the `SteamVR_BasicTeleport` script can be viewed in the
scene `Examples/004_CameraRig_BasicTeleport`. The scene uses
the `SteamVR_SimplePointer` script on the Controllers to initiate a
laser pointer with the Controller `Grip` button and when the laser
pointer is deactivated (release the `Grip`) then the player is
teleported to the location of the laser pointer tip.

#### Height Adjustable Teleporter (SteamVR_HeightAdjustTeleport)

The height adjust teleporter extends the basic teleporter and allows
for the y position of the `[CameraRig]` to be altered based on whether
the teleport location is on top of another object.

Like the basic teleporter the Height Adjust Teleport script is attached
to the `[CameraRig]` prefab and requires a World Pointer to be
available.

The following script parameters are available:

  * **Blink Transition Speed:** The fade blink speed on teleport
  * **Headset Position Compensation:** If this is checked then the
  teleported location will be the position of the headset within the
  play area. If it is unchecked then the teleported location will
  always be the centre of the play area even if the headset position
  is not in the centre of the play area.
  * **Play Space Falling:** Checks if the player steps off an object
  into a part of their play area that is not on the object then they are
  automatically teleported down to the nearest floor.

The `Play Space Falling` option also works in the opposite way that if
the player's headset is above an object then the player is teleported
automatically on top of that object, which is useful for simulating
climbing stairs without needing to use the pointer beam location. If this
option is turned off then the player can hover in mid air at
the same y position of the object they are standing on.

An example of the `SteamVR_HeightAdjustTeleport` script can be viewed
in the scene `Examples/007_CameraRig_HeightAdjustTeleport`. The scene
has a collection of varying height objects that the player can either
walk up and down or use the laser pointer to climb on top of them.

Another example can be viewed in the scene
`Examples/010_CameraRig_TerrainTeleporting` which shows how the
teleportation of a player can also traverse terrain colliders.

#### Fading On Headset Collision (SteamVR_HeadsetCollisionFade)

The purpose of the Headset Collision Fade is to detect when the user's
VR headset collides with another game object and fades the screen to
a solid colour. This is to deal with a player putting their head into
a game object and seeing the inside of the object clipping, which is
an undesired effect.

The reasoning behind this is if the player puts their head where it
shouldn't be, then fading to a colour (e.g. black) will make the
player realise they've done something wrong and they'll probably
naturally step backwards.

The Headset Collision Fade script is attached to the `Camera (head)`
object within the `[CameraRig]` prefab.

The following script parameters are available:

  * **Blink Transition Speed:** The fade blink speed on collision.
  * **Fade Color:** The colour to fade the headset to on collision.

An example of the `SteamVR_HeadsetCollisionFade` script can be
viewed in the scene `Examples/011_Camera_HeadSetCollisionFading`.
The scene has collidable walls around the play area and if the player
puts their head into any of the walls then the headset will fade to
black.

#### Interactable Object (SteamVR_InteractableObject)

The Interactable Object script is attached to any game object that is
required to be interacted with (e.g. via the controllers).

The following script parameters are available:

  * **Is Grabbable:** Determines if the object can be grabbed
  * **Hold Button To Grab:** If this is checked then the grab button
  on the controller needs to be continually held down to keep grabbing.
  If this is unchecked the grab button toggles the grab action with
  one button press to grab and another to release.
  * **Is Usable:** Determines if the object can be used
  * **Hold Button To Use:** If this is checked then the use button
  on the controller needs to be continually held down to keep using.
  If this is unchecked the the use button toggles the use action with
  one button press to start using and another to stop using.
  * **Highlight On Touch:** The object will only highlight when a
  controller touches it if this is checked.
  * **Touch Highligt Color:** The colour to highlight the object
  when it is touched. This colour will override any globally set
  color (for instance on the `SteamVR_InteractTouch` script).
  * **Grab Snap Type:** This sets the snap type of the object when
  it is grabbed.
   * `Simple_Snap` snaps the grabbed object's central position to the
   controller attach point (default is controller tip).
   * `Rotation_Snap` snaps the grabbed object to a specific rotation
   which is provided as a Vector3 in the `Snap To Rotation` parameter.
   * `Precision_Snap` does not snap the object's position to the
   controller and picks the object up at the point the controller is
   touching the object (like a real life hand picking something up).
  * **Snap To Rotation:** A Vector3 of EulerAngles that determines the
  rotation of the object in relation to the controller on snap.
  This is useful for picking up guns or swords where the relative
  rotation to the controller is important for ease of use.

The basis of this script is to provide a simple mechanism for
identifying objects in the game world that can be grabbed or used
but it is expected that this script is the base to be inherited into a
script with richer functionality.

An example of the `SteamVR_InteractableObject` can be viewed in the
scene `Examples/005_Controller_BasicObjectGrabbing`. The scene
also uses the `SteamVR_InteractTouch` and `SteamVR_InteractGrab`
scripts on the controllers to show how an interactable object can be
grabbed and snapped to the controller and thrown around the game world.

Another example can be viewed in the scene
`Examples/013_Controller_UsingAndGrabbingMultipleObjects`. The scene
shows mutltiple objects that can be grabbed by holding the buttons
or grabbed by toggling the button click and also has objects that
can have their Using state toggled to show how mutliple items can be
turned on at the same time.

#### Touching Interactable Objects (SteamVR_InteractTouch)

The Interact Touch script is attached to a Controller object within the
`[CameraRig]` prefab.

The following script parameters are available:

  * **Hide Controller On Touch**: Hides the controller model when a valid
  touch occurs
  * **Global Touch Highlight Color:** If the interactable object can be
  highlighted when it's touched but no local colour is set then this
  global colour is used.

The following events are emitted:

  * **ControllerTouchInteractableObject:** Emitted when a valid object is
  touched
  * **ControllerUntouchInteractableObject:** Emitted when a valid object
  is no longer being touched

The event payload that is emitted contains:

  * **controllerIndex:** The index of the controller doing the interaction
  * **target:** The GameObject of the interactable object that is being
  interacted with by the controller

An example of the `SteamVR_InteractTouch` can be viewed in the
scene `Examples/005_Controller/BasicObjectGrabbing`. The scene
demonstrates the highlighting of objects that have the
`SteamVR_InteractableObject` script added to them to show the
ability to highlight interactable objects when they are touched by
the controllers.

#### Grabbing Interactable Objects (SteamVR_InteractGrab)

The Interact Grab script is attached to a Controller object
within the `[CameraRig]` prefab and the Controller object
requires the `SteamVR_ControllerEvents` script to be attached as it
uses this for listening to the controller button events for grabbing
and releasing interactable game objects. It listens for the
`AliasGrabOn` and `AliasGrabOff` events to determine when an object
should be grabbed and should be released.

The Controller object also requires the `SteamVR_InteractTouch` script
to be attached to it as this is used to determine when an interactable
object is being touched. Only valid touched objects can be grabbed.

An object can be grabbed if the Controller touches a game object which
contains the `SteamVR_InteractableObject` script and has the flag
`isGrabbable` set to `true`.

If a valid interactable object is grabbable then pressing the set
`Grab` button on the Controller (default is `Trigger`) will grab and
snap the object to the controller and will not release it until the
`Grab` button is released.

When the Controller `Grab` button is released, if the interactable
game object is grabbable then it will be propelled in the direction
and at the velocity the controller was at, which can simulate object
throwing.

The interactable objects require a collider to activate the trigger and
a rigidbody to pick them up and move them around the game world.

The following script parameters are available:

  * **Hide Controller On Grab:** Hides the controller model when a valid
  grab occurs
  * **Controller Attach Point:** The rigidbody point on the controller
  model to snap the grabbed object to (defaults to the tip)

The following events are emitted:

  * **ControllerGrabInteractableObject:** Emitted when a valid object is
  grabbed
  * **ControllerUngrabInteractableObject:** Emitted when a valid object
  is released from being grabbed

The event payload that is emitted contains:

  * **controllerIndex:** The index of the controller doing the interaction
  * **target:** The GameObject of the interactable object that is being
  interacted with by the controller

An example of the `SteamVR_InteractGrab` can be viewed in the
scene `Examples/005_Controller/BasicObjectGrabbing`. The scene
demonstrates the grabbing of interactable objects that have the
`SteamVR_InteractableObject` script attached to them. The objects
can be picked up and thrown around.

More complex examples can be viewed in the scene
`Examples/013_Controller_UsingAndGrabbingMultipleObjects` which
demonstrates that each controller can grab and use objects
independently and objects can also be toggled to their use state
simultaneously. The scene
`Examples/014_Controller_SnappingObjectsOnGrab` demonstrates
the different mechanisms for snapping a grabbed object to the
controller.

#### Using Interactable Objects (SteamVR_InteractUse)

The Interact Use script is attached to a Controller object
within the `[CameraRig]` prefab and the Controller object
requires the `SteamVR_ControllerEvents` script to be attached as it
uses this for listening to the controller button events for using
and stop using interactable game objects. It listens for the
`AliasUseOn` and `AliasUseOff` events to determine when an object
should be used and should stop using.

The Controller object also requires the `SteamVR_InteractTouch` script
to be attached to it as this is used to determine when an interactable
object is being touched. Only valid touched objects can be used.

An object can be used if the Controller touches a game object which
contains the `SteamVR_InteractableObject` script and has the flag
`isUsable` set to `true`.

If a valid interactable object is usable then pressing the set
`Use` button on the Controller (default is `Trigger`) will call the
`StartUsing` method on the touched interactable object.

The following script parameters are available:

  * **Hide Controller On Use:** Hides the controller model when a valid
  use action starts

The following events are emitted:

  * **ControllerUseInteractableObject:** Emitted when a valid object starts
  being used
  * **ControllerUnuseInteractableObject:** Emitted when a valid object
  stops being used

The event payload that is emitted contains:

  * **controllerIndex:** The index of the controller doing the interaction
  * **target:** The GameObject of the interactable object that is being
  interacted with by the controller

An example can be viewed in the scene
`Examples/006_Controller_UsingADoor`. Which simulates using
a door object to open and close it. It also has a cube on the floor
that can be grabbed to show how interactable objects can be usable
or grabbable.

Another example can be viewed in the scene
`Examples/008_Controller_UsingAGrabbedObject` which shows that objects
can be grabbed with one button and used with another (e.g. firing a
gun).

#### Abstract Classes

To allow for reusablity and object consistency, a collection of
abstract classes are provided which can be used to extend into a
concrete class providing consistent functionality across many
different scripts without needing to duplicate code.

The current abstract classes are available:

##### SteamVR_WorldPointer

This abstract class provides any game pointer the ability to know the
the state of the implemented pointer and emit an event to other scripts
in the game world.

The World Pointer also provides a play area cursor to be displayed for
all cursors that utilise this class. The play area cursor is a
representation of the current calibrated play area space and is useful
for visualising the potential new play area space in the game world
prior to teleporting. It can also handle collisions with objects on the
new play area space and prevent teleporting if there are any collisions
with objects at the potential new destination.

The play area collider does not work well with terrains as they are
uneven and cause collisions regularly so it is recommended that
handling play area collisions is not enabled when using terrains.

The following script parameters are available:

  * **Pointer Hit Color:** The colour of the beam when it is colliding
  with a valid target. It can be set to a different colour for each
  controller.
  * **Pointer Miss Color:** The colour of the beam when it is not
  hitting a valid target. It can be set to a different colour for each
  controller.
  * **Show Play Area Cursor:** If this is enabled then the play area
  boundaries are displayed at the tip of the pointer beam in the
  current pointer colour.
  * **Handle Play Area Cursor Collisions:** If this is ticked then if
  the play area cursor is colliding with any other object then the
  pointer colour will change to the `Pointer Miss Color` and the
  `WorldPointerDestinationSet` event will not be triggered, which will
  prevent teleporting into areas where the play area will collide.
  * **Pointer Facing Axis:** The facing axis can also be set to match
  the direction the `[CameraRig`] Prefab is facing as if it is rotated
  then the beam will emit out of the controller at the wrong angle, so
  this setting can be adjusted to ensure the beam always projects
  forward.

The following events are emitted:

  * **WorldPointerIn:** When the pointer collides with another
  game object.
  * **WorldPointerOut:** When the pointer stops colliding with
  the game object.
  * **WorldPointerDestinationSet:** When the pointer is no longer
  active in the scene to determine the last destination position of
  the pointer end (useful for selecting and teleporting).

The event payload that is emitted contains:

  * **controllerIndex:** The index of the controller emitting the beam
  * **distance:** The distance the target is from the controller
  * **target:** The Transform of the object that the pointer is touching
  * **tipPosition:** The world position of the end of the pointer

### Examples

This directory contains Unity3d scenes that demonstrate the scripts
and prefabs being used in the game world to create desired
functionality.

There is also a `/Scripts` directory within the `/Examples` directory
that contains helper scripts utilised by the example scenes to
highlight certain functionality (such as event listeners). These
example scripts are not required for real world usage.

The current examples are:

  * **001_CameraRig_VR_PlayArea:** A simple scene showing the `[CameraRig]`
  prefab usage.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=hr5OoSCksnY)

  * **002_Controller_Events:** A simple scene displaying the events from
  the controller in the console window.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=B-YtXomrBBI)

  * **003_Controller_SimplePointer:** A scene with basic objects that can
  be pointed at with the laser beam from the controller activated by
  the `Grip` button. The pointer events are also displayed in the
  console window.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=2DqFTfbf22c)

  * **004_CameraRig_BasicTeleport:** A scene with basic objects that can
  be traversed using the controller laser beam to point at an object
  in the game world where the player is to be teleported to by
  pressing the controller `Grip` button. When the `Grip` button is
  released, the player is teleported to the laser beam end location.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=dbbNPPX-R6E)

  * **005_Controller_BasicObjectGrabbing:** A scene with a selection of
  objects that can be grabbed by touching them with the controller and
  pressing the `Trigger` button down. Releasing the trigger button
  will propel the object in the direction and velocity of the grabbing
  controller. The scene also demonstrates simple highlighting of
  objects when the controller touches them. The interaction events are
  also displayed in the console window.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=FjwN8AJx0rY)

  * **006_Controller_UsingADoor:** A scene with a door interactable
  object that is set to `usable` and when the door is used by pressing
  the controller `Trigger` button, the door swings open (or closes if
  it's already open).
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=lxDjkmILzpY)

  * **007_CameraRig_HeightAdjustTeleport:** A scene with a selection of
  varying height objects that can be traversed using the controller
  laser beam to point at an object and if the laser beam is pointing
  on top of the object then the player is teleported to the top of the
  object. Also, it shows that if the player steps into a part of the
  play area that is not on the object then the player will fall to
  the nearest object. This also enables the player to climb objects
  just by standing over them as the floor detection is done from the
  position of the headset.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=4WJ9AyDABJo)

  * **008_Controller_UsingAGrabbedObject:** A scene with interactable
  objects that can be grabbed (pressing the `Grip` controller button)
  and then used (pressing the `Trigger` controller button). There is
  a gun on a table that can be picked up and fired, or a strange box
  that when picked up and used the top spins.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=mhVx7kfLSe8)

  * **009_Controller_BezierPointer:** A scene with a selection of
  varying height objects that can be traversed using the controller
  however, rather than just pointing a straight beam, the beam is
  curved (over a bezier curve) which allows climbing on top of items
  that the player cannot visibly see.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=oOZV4bxdw5o)

  * **010_CameraRig_TerrainTeleporting:** A scene with a terrain
  object and a selection of varying height 3d objects that can be
  traversed using the controller laser beam pointer. It shows how the
  Height Adjust Teleporter can be used to climb up and down game
  objects as well as traversing terrains as well.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=CzKohhSjXNY)

  * **011_Camera_HeadSetCollisionFading:** A scene with three walls
  around the play area and if the player puts their head into any
  of the collidable walls then the headset fades to black to prevent
  seeing unwanted object clipping artifacts.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=r0RZci0tZOI)

  * **012_Controller_PointerWithAreaCollision:** A scene which
  demonstrates how to use a controller pointer to traverse a world
  but where the beam shows the projected play area space and if
  the space collides with any objects then the teleportation
  action is disabled. This means it's possible to create a level
  with areas where the user cannot teleport to because they would
  allow the player to clip into objects.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=OwACH7nhW1Q)

  * **013_Controller_UsingAndGrabbingMultipleObjects:** A scene which
  demonstrates how interactable objects can be grabbed by holding down
  the grab button continuously or by pressing the grab button once to
  pick up and once again to release. The scene also shows that the use
  button can have a hold down to keep using or a press use button once
  to start using and press again to stop using. This allows multiple
  objects to be put into their Using state at the same time as also
  demonstrated in this example scene.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=6ySXa569UOw)

  * **014_Controller_SnappingObjectsOnGrab:** A scene with a selection
  of objects that demonstrate the different snap to controller
  mechanics. The two green guns, green lightsaber and sword all
  utilise the `Rotation Snap` which orientates the object into a
  specific given rotation to ensure the object feels like it's been
  held naturally in the hand. The red gun utilises the `Simple Snap`
  which does not affect the object's rotation but positions the centre
  of the object to the snap point on the controller. The red/green gun
  utilises the `Precision Snap` which does not affect the rotation or
  position of the grabbed object and picks the object up at the point
  that the controller snap point is touching the object.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=zLBlef1ikLE)

  * **015_Controller_TouchpadAxisControl:** A scene with an R/C car
  that is controlled by using the Controller Touchpad. Moving a finger
  up and down on the Touchpad will cause the car to drive forward or
  backward. Moving a finger to the left or right of the Touchpad will
  cause the car to turn in that direction. Pressing the Trigger will
  cause the car to jump, this utilises the Trigger axis and the more
  the trigger is depressed, the higher the car will jump.
   * [View Example Tour on Youtube](https://www.youtube.com/watch?v=4J8abeLzH58)

## Contributing

I'd love this to be a community effort, but as I'm just getting
started on this, it may be best to leave pull requests for now on
new features until I get my head around how this is going to
progress. I'm happy for bug fix pull requests though :)

Also, if you find any issues or have any suggestions then please
raise an issue on GitHub and I'll take a look when I can.

## License

Code released under the MIT license.
