# Gamepad
.Net Core based library, that allows getting the button presses and axis value changes of a gamepad or a Joystick.

The library was developed with Raspberry Pi in mind (but probably runs on any linux based device)

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


If you'd like to support this project and the addition of support for other Adafruit HATs, please donate money.

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donate_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=UK9A88VUXL9YJ&currency_code=CAD&source=url)
