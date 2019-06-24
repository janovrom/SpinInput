## Spin Input
So I was wondering, why should I use the standard soft keyboard on Android. It's annoying as hell as you can't use it to write with more than two fingers, though there are the swipe inputs and you can easily get proficient with it. This is where the idea came in: you use just 2 fingers and the keyboard occludes too much of the space, so why not to shift it to only two corners.

![](http://janovrom.ddns.net/janovrom/SpinInput/raw/master/Media/Screenshot_2019-06-24-10-39-28.png =75x)
![](http://janovrom.ddns.net/janovrom/SpinInput/raw/master/Media/Screenshot_2019-06-24-10-40-29.png =75x)

So the spinner is implemented in Unity camera space and its keys are placed simply on cylinder in different depth. The cylinder is aligned to the camera position and its position on the screen to allow the perspective projection take care of the depth perception and sizing. That's why it can be used in VR since the UI elements are not simply placed on screen. 