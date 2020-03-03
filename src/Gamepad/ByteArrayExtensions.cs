using System;

namespace Gamepad
{
    public static class ByteArrayExtensions
    {
        public static bool HasConfiguration(this byte[] message)
        {
            return IsFlagSet(message[6], 0x80); // 0x80 in byte 6 means it has Configuration information
        }

        public static bool IsButton(this byte[] message)
        {
            return IsFlagSet(message[6], 0x01); // 0x01 in byte 6 means it is a Button
        }

        public static bool IsAxis(this byte[] message)
        {
            return IsFlagSet(message[6], 0x02); // 0x01 in byte 6 means it is a Axis
        }

        public static bool IsButtonPressed(this byte[] message)
        {
            return message[4] == 0x01; // byte 4 contains the status (0x01 means pressed, 0x00 means released)
        }

        public static byte GetAddress(this byte[] message)
        {
            return message[7]; // Address is stored in byte 7
        }

        public static short GetAxisValue(this byte[] message)
        {
            return BitConverter.ToInt16(new byte[2] { message[4], message[5] }, 0); // Value is stored in bytes 4 and 5
        }

        /// <summary>
        /// Checks if bits that are set in flag are set in value.
        /// </summary>
        private static bool IsFlagSet(byte value, byte flag)
        {
            byte c = (byte)(value & flag);
            return c == flag;
        }
    }
}
