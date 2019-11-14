using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Gamepad.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // If the app is called with "debug" as first argument, it will wait in the following loop for
            // you to attach to the process to debug. Add a breakpoint within the while, and once you
            // attach you connect the debugger to the process, manually move the execution outside the while body
            // so the application can run with the debugger attached from the beginning
            if (args.Length > 0 && args[0].ToLower().Contains("debug"))
            {
                Console.WriteLine("Waiting for debugger to attach...");
                while (!Debugger.IsAttached)
                {
                    Thread.Sleep(100);
                }
                Console.WriteLine("Debugger attached.");
            }

            using (var gamepad = new GamepadController("/dev/input/js0"))
            {
                gamepad.ButtonChanged += (object sender, ButtonEventArgs e) =>
                {
                    Console.WriteLine($"Button {e.Button} Pressed: {e.Pressed}");
                };

                gamepad.AxisChanged += (object sender, AxisEventArgs e) =>
                {
                    Console.WriteLine($"Axis {e.Axis} Pressed: {e.Value}");
                };

                Console.ReadLine();
            }
        }
    }
}
