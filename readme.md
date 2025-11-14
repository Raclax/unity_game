# Required applications
- Unity Hub
	- https://unity.com/download
- Meta Developer Hub 
	- https://developers.meta.com/horizon/downloads/package/oculus-developer-hub-win/
- MetaQuestLink
	- https://www.meta.com/en-gb/help/quest/1517439565442928/
- SideQuest
	- https://sidequestvr.com/setup-howto
- Any IDE for coding (recommended VS or VS Code)
# Setting up the project
1. Have Unity hub setup including the license
2. Clone the Gitlab repository of the project
3. Open the project with Unity Hub
4.  Ensure you have the proper Unity Editor version (6000.0.58f2) with the Android SDK (check course slides)
# Running the project
Once the project is open, it should be good to go to run by clicking the play button. This will cause the Avatar to spawn but not interaction is possible.
Next to the play button of the scene, there is a small computer icon alongside Meta's icon. 
- Click the computer icon (turns blue when active) to turn on the Simulator for fast debugging on PC when developing. 
- Now pressing play to run the scene will create a new window, with this menu you can see the perspective of the player (left eye by default) and you can interact with objects as if you were using the HMD
# Building the project
Stop the scene if it is playing. 
go to Edit -> Project Settings -> XR Plug-in Management
1. Make sure that **Oculus** is selected as the Plug-in Providers for both the android and PC options.
Under XR Plug-in Management -> Project Validation 
2. Make sure no major errors/issues are present
Once you checked 1. and 2. you can build the project by going to File -> Build Profiles
3. Make sure your scene is presented and selected in the Scene List
4. Pressing Build will then prompt you to select the directory to save the build in. 
	-  It is good practice to have builds inside a dedicated folder inside the Root directory of the project (the builds folder excluded in gitignore)
5. If the build requires a key create your own, save it on your pc with your use and password
	- Key Manager can be found under Project settings -> Player -> Publishing Settings -> Keystore Manager
6. Once the build is complete, you can check if it was successful by checking the console of the project
7. Connect your headset and proceed to install your build (.APK file) on your HMD using Meta Developer Hub OR SideQuest.
# Overview of the Scene
The scene is split into 3 main groups of gameobjects
## Managers
Managers are objects that hold scripts that organize and handle operations, they are invisible objects in the scene that handle things and not actually interactable. 
### AvatarSdkManager
Holds the scripts that handle creating and visualizing the Meta Avatar. Imported from the Meta Avatar SDK
### LogsManager
This script handles logging events and positions from other objects. 
- It is a Global Object (Singleton) which means there should be only one of it in the scene. You can call it from any other script. 
- It holds a Queue of all events to log and will update them at the end of every frame.
- The logs are saved into a JSON file that is named based on the date and time of recording start. You can find the file:
	- On PC: Check the console logs at the end of a session to find the file
	- On HMD: Connect the headset to PC and use SideQuest to access the files on the headset, go to Android -> data -> *$YOURAPPNAME*(by default it's: com.VICO.VRClassroom) -> files -> logs
Important Functions are:
	1. LogEvent(string message)
	2. LogTransform(Transform transform, string label)
### EventSystem
Holds the scripts related to XR Interaction toolkit, this is the script that handles the XR user input, and translates user inputs through interactors to the interactable objects.
(https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@3.2/manual/index.html)
## Player
The player object holds 2 child objects;
### XR_OVR_Origin
This is the object that handles all the player scripts
- Input Action Manager: Related to XR interaction toolkit, it handles capturing input from the user
- OVRCameraRig: Holds scripts that are specific to Meta Quest HMDs. 
	- Tracking Space: actual hierarchy of objects that are connected and tracked by the headset.
		- LeftEyeAnchor, RightEyeAnchor, CenterEyeAnchor: contains the cameras (right, left and a virtual center camera), the left and right are the actual camera that the user will be using when using the HMD.
		- LeftHandAnchor: Connects to the left hand of the user and contains the interactors that are attached to the hand (ray interactor, direct interactor, teleport interactor..)
		- RightHandAnchor: Connects to the right hand of the user and contains the interactors that are attached to the hand (ray interactor, direct interactor, teleport interactor..)
	- CharacterController: is responsible of moving the player and detecting body collision of the player with objects (floors or walls..)
- Locomotion: Contains the hierarchy of scripts that handles player movement
	- Several Locomotion techniques are available
		- Sliding using the left hand joystick
		- Turning (snap or continuous) using the joystick
		- Teleportation using the right hand joystick
	- Other locomotion methods are available but turned off by default
### Avatar Visual
This is the object that communicates with the AvatarSdkManager and actually gets visualized. There are two objects 
	First Person Avatar: The player will see this avatar when they look at their hands and body
	Third Person Avatar: The player will see this avatar when they look at themselves in the mirror
This choice of having to avatars that are ontop of each other is due to the fact that in VR the model you embody is incomplete. The embodied avatar has some parts (head, neck, legs) removed not to obstruct the camera's Field of view and preserve performance
## Environment
The environment contains a variety of objects that user can and can't interact with, as well as 3D UI elements.
### Interactables
interactables hold the following:
-  rigidbody for physics collision detection with the player
	- A collider that is a child (or same object) of the object holding the rigidbody
- They hold XR Interactable script that is part of XR interaction tool kit. It is used to customize what happens when an interaction with this object is detected. Several options for XR interactable scripts
	- XR Grab Interactable
	- XR Simple Interactable
	- XR General Grab Interactable
- A visual model that represents the shape of the object.
### UI
Several UI menu examples are available to showcase how to design buttons, slides, drop menus and toggles.
All UI elements belong to a Canvas with the graphic raycaster component to handle user input. 
Another UI element present is the interactable Video Player.
### Teleportation Area
In order to be able to teleport on the floor object, the Teleportation Area script should be setup
With the list of colliders that allow to be teleported on defined in the "Colliders" list of the script.
More over, Teleportation Anchors can be defined using the "Teleportation Anchor" scripts. These anchors will allow the player to teleport to them and fix them in space, such as interacting with a certain chair in front of a  workbench.
### Mirror
The Mirror is done by having a Camera that acutally captures what is in front of the mirror and outputs what the Camera sees onto the "Mirror Visual" Object (Quad). The actual Mirror is surronded by a frame. 
A script that manages the Mirror is called "Mirror Camera Config", you can choose between several options for the quality of the image that is being displayed on the mirror (by default it is set to medium).
### Non-Interactable Objects
These are objects that are the user can't interact with. 
- They could have colliders such as walls and floors. 
- Objects that hold the light emitting components such as spot lights, directional lights..
### Lights
Make sure to have only 1 realtime light in the scene, the rest should be baked lights, that choice is mainly related to performance, feel free to try and test different types of lights to see what fits your scene the best. Remember that for baked lights to actually work you need to actually compute them before hand, by going in Unity to Window -> Rendering -> Lights and clicking Generate Lighting. 
# Designing your Project
## Create your own Scene

### Player
- Make sure you use the XR_OVR_Origin prefab to properly connect with the headset. 
- Make sure to use the Avatar Visual prefab in order to properly visualize the Avatar of the user.
### Managers
All managers found in the sample scene provided should be placed in your scene in order to: 
- **AvatarSdkManager**: Handles Creation of Meta Avatar
- **LogsManager**: Handles Logging Metrics you define
- **EventSystem**: Handles interactions between user and environment
### Building your Scene
You can use all the assets and objects available in the sample scene. You can modify existing assets and import/create new assets. 
Make sure to continuous test your application on the HMD to try out the interactions, collisions, etc..
### Creating Interactable Objects
Following how interactable objects are designed in the main scene, you can import your own objects or reuse existing ones. A reminder that interactables hold the following:
-  rigidbody for physics collision detection with the player
	- A collider that is a child (or same object) of the object holding the rigidbody
- XR Interactable script (https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/xr-grab-interactable.html)
	- XR Grab Interactable
	- XR Simple Interactable
	- XR General Grab Interactable
- Visual model
With the XR interactable script, you will find a a list called interactable Events, exposing the list will reveal a set of empty lists, each belonging to a certain "Interaction Event".
Inside these lists, let's take an example of Hover. You can find HoverEnter and HoverExit. (https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@2.0/manual/interactable-events.html)
In this case, when this interactable object detects that an interactor is hovering it (Player hand or ray), it will trigger and call all actions that are inside the HoverEnter list. 
- Let's say I want to play a sound when the play hovers my object, then I will add a new entry in the HoverEnter list. Inside this list I will add my AudioSource Component, and then provide the function that plays the Audio by the AudioSource
- Another Example, I want to change the color of the material of the object when it is hovered. Then I will create a new entry in the HoverEntered, add the script that handles changing the color of material and precise the function I want to call (example "SwapColor()")
## Logging Events and Objects
### Logging an Object
In order to continuously log the position and rotation of an object in 3D space, you attach "Log This Object" script to the object. 
	Provide a label in the Log This Object script, example: Label of the head object = "Head", while the label of left hand is = "Left Hand", so that you can identify them later on during analysis
	Provide the LoggingPeriod, as wait for LoggingPeriod before logging. Example: if Loggingperiod is 1, a log will be saved every 1 sec. A loggingperiod of 0 will cause the log to be saved every frame
### Logging an Event
In order to log an event of your choice, you attach "Log This Event" script to the object. 
Then provide a Message (string) that will resemble the content of the message to be logged. Moreover you can add a cooldown so that you don't log several consecutive logs, example hovering over a certain object will have a 3 section cooldown before it logs a second message.
Once you defined the parameters of Log This Event, you have to call the "LogEvent" function in order to create the log. Example, I want to detect when a player hovers a certain object, I go to the interactable object events, to the HoverEnter (or HoverExit) and I create a new entry, in this entry I attach the "Log This Event" script and I call LogEvent() function on it.
## Debugging 
### Meta Simulator
Next to the play button of the scene, there is a small computer icon alongside Meta's icon. 
- Click the computer icon (turns blue when active) to turn on the Simulator for fast debugging on PC when developing. 
- Now pressing play to run the scene will create a new window, with this menu you can see the perspective of the player (left eye by default) and you can interact with objects as if you were using the HMD
### Meta Quest Link
This method should be favored for better autonomy, better performance/fluidity and faster launch of simulations.
- After configuring the project and installing the necessary applications,
you can decide to connect the headset directly via a USB cable (Meta Quest Link), or wirelessly via the AirLink functionality.
- Put on your headset and access the quick actions panel and then choose
your connection method to connect to your PC. You will then be redirected to the menu of the Oculus software installed on your PC.
- All you have to do is place yourself in the scene Menu and start
the simulation. However, make sure you have added any new scenes to the
compiler (File > Build Settings > Add Open Scenes).
(https://www.meta.com/en-gb/help/quest/509273027107091/)
