using System;

namespace Gamepad
{
    public interface IGamepadController : IDisposable
    {
        event EventHandler<ButtonEventArgs> ButtonChanged;
        event EventHandler<AxisEventArgs> AxisChanged;
    }
}