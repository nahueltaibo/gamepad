using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Gamepad
{
    public partial class GamepadController : IGamepadController
    {
        private readonly string _deviceFile;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public Dictionary<byte, bool> Buttons = new Dictionary<byte, bool>();
        public Dictionary<byte, short> Axis = new Dictionary<byte, short>();

        /// <summary>
        /// EventHandler to allow the notification of Button changes.
        /// </summary>
        public event EventHandler<ButtonEventArgs> ButtonChanged;

        /// <summary>
        /// EventHandler to allow the notification of Axis changes.
        /// </summary>
        public event EventHandler<AxisEventArgs> AxisChanged;

        public GamepadController(string deviceFile = "/dev/input/js0")
        {
            if (!File.Exists(deviceFile))
            {
                throw new ArgumentException(nameof(deviceFile), $"The device {deviceFile} does not exist");
            }

            _deviceFile = deviceFile;

            // Create the Task that will constantly read the device file, process its bytes and fire events accordingly
            _cancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() => ProcessMessages(_cancellationTokenSource.Token));
        }

        private void ProcessMessages(CancellationToken token)
        {
            using (FileStream fs = new FileStream(_deviceFile, FileMode.Open))
            {
                byte[] message = new byte[8];

                while (!token.IsCancellationRequested)
                {
                    // Read chunks of 8 bytes at a time.
                    fs.Read(message, 0, 8);

                    if (message.HasConfiguration())
                    {
                        ProcessConfiguration(message);
                    }

                    ProcessValues(message);
                }
            }
        }

        private void ProcessConfiguration(byte[] message)
        {
            if (message.IsButton())
            {
                byte key = message.GetAddress();
                if (!Buttons.ContainsKey(key))
                {
                    Buttons.Add(key, false);
                    return;
                }
            }
            else if (message.IsAxis())
            {
                byte key = message.GetAddress();
                if (!Axis.ContainsKey(key))
                {
                    Axis.Add(key, 0);
                    return;
                }
            }
        }

        private void ProcessValues(byte[] message)
        {
            if (message.IsButton())
            {
                var oldValue = Buttons[message.GetAddress()];
                var newValue = message.IsButtonPressed();

                if (oldValue != newValue)
                {
                    Buttons[message.GetAddress()] = message.IsButtonPressed();
                    ButtonChanged?.Invoke(this, new ButtonEventArgs { Button = message.GetAddress(), Pressed = newValue });
                }
            }
            else if (message.IsAxis())
            {
                var oldValue = Axis[message.GetAddress()];
                var newValue = message.GetAxisValue();

                if (oldValue != newValue)
                {
                    Axis[message.GetAddress()] = message.GetAxisValue();
                    AxisChanged?.Invoke(this, new AxisEventArgs { Axis = message.GetAddress(), Value = newValue });
                }
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
