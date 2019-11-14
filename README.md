# gamepad
.Net Core Gamepad library, that allows getting the button presses and axis value changes of a gamepad or a Joystick in Raspberry Pi (and probably anything running linux)

This is all you need to do to use it:

```C#
// You should provide the gamepad file you want to connect to. /dev/input/js0 is the default
using (var gamepad = new GamepadController("/dev/input/js0")) 
{
    // Configure this if you want to get events when the state of a button changes
    gamepad.ButtonChanged += (object sender, ButtonEventArgs e) =>
    {
        Console.WriteLine($"Button {e.Button} Pressed: {e.Pressed}");
    };

    // Configure this if you want to get events when the state of an axis changes
    gamepad.AxisChanged += (object sender, AxisEventArgs e) =>
    {
        Console.WriteLine($"Axis {e.Axis} Pressed: {e.Value}");
    };

    Console.ReadLine();
}
// Remember to Dispose the GamepadController, so it can finish the Task that listens for changes in the gamepad
```
