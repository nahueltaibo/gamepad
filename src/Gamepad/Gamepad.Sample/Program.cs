using System;

namespace Gamepad.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // You should provide the gamepad file you want to connect to. /dev/input/js0 is the default
            using (var gamepad = new GamepadController("/dev/input/js0"))
            {
                Console.WriteLine("Start pushing the buttons/axis of your gamepad/joystick to see the output");

                // Configure this if you want to get events when the state of a button changes
                gamepad.ButtonChanged += (object sender, ButtonEventArgs e) =>
                {
                    Console.WriteLine($"Button {e.Button} Changed: {e.Pressed}");
                };

                // Configure this if you want to get events when the state of an axis changes
                gamepad.AxisChanged += (object sender, AxisEventArgs e) =>
                {
                    Console.WriteLine($"Axis {e.Axis} Changed: {e.Value}");
                };

                Console.ReadLine();
            }
            // Remember to Dispose the GamepadController, so it can finish the Task that listens for changes in the gamepad
        }
    }
}
