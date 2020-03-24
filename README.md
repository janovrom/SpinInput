## Spin Input
So I was wondering, why should I use the standard soft keyboard on Android. It's annoying as hell as you can't use it to write with more than two fingers, though there are the swipe inputs and you can easily get proficient with it. This is where the idea came in: you use just 2 fingers and the keyboard occludes too much of the space, so why not to shift it to only two corners.

![](https://github.com/janovrom/SpinInput/tree/master/Media/Screenshot_2019-06-24-10-39-28.png)
![](https://github.com/janovrom/SpinInput/tree/master/Media/Screenshot_2019-06-24-10-40-29.png)

So the spinner is implemented in Unity camera space and its keys are placed simply on cylinder in different depth. The cylinder is automatically aligned w.r.t. the camera position and its position on the screen to allow the perspective projection to take care of the depth perception and sizing. That's why it can be used in VR since the UI elements are not simply placed on screen. 
The character sets can be easily switched as they are implemented as ScriptableObject, so it's only a matter of changing references.

I was quite shameless about coding the input field as it is made by stripping the old UI InputField (Unity 5.3. made public on [github](https://github.com/tenpn/unity3d-ui/blob/master/UnityEngine.UI/UI/Core/InputField.cs)) of all stuff regarding virtual keyboards (and obsolete calls). The input was replaced by sending virtual KeyEvents to active input field.

TODO: Requires some user testing (positions of buttons, its sizes, etc.)
