using System;
using System.Runtime.InteropServices;

public class KeyboardController
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray)] INPUT[] pInputs, int cbSize);

    [StructLayout(LayoutKind.Sequential)]
    private struct INPUT
    {
        public uint type;
        public MOUSEKEYBDHARDWAREINPUT mkhi;
    }

    [StructLayout(LayoutKind.Explicit)]
    private struct MOUSEKEYBDHARDWAREINPUT
    {
        [FieldOffset(0)]
        public KEYBDINPUT ki;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    private const uint INPUT_KEYBOARD = 1;
    private const uint KEYEVENTF_KEYUP = 0x0002;

    public static void SendKey(ushort keyCode)
    {
        // Create INPUT structure for key press
        INPUT[] inputs = new INPUT[2];

        // Key Down
        inputs[0].type = INPUT_KEYBOARD;
        inputs[0].mkhi.ki.wVk = keyCode;
        inputs[0].mkhi.ki.wScan = 0;
        inputs[0].mkhi.ki.dwFlags = 0;
        inputs[0].mkhi.ki.time = 0;
        inputs[0].mkhi.ki.dwExtraInfo = IntPtr.Zero;

        // Key Up
        inputs[1].type = INPUT_KEYBOARD;
        inputs[1].mkhi.ki.wVk = keyCode;
        inputs[1].mkhi.ki.wScan = 0;
        inputs[1].mkhi.ki.dwFlags = KEYEVENTF_KEYUP;
        inputs[1].mkhi.ki.time = 0;
        inputs[1].mkhi.ki.dwExtraInfo = IntPtr.Zero;

        // Send the input
        SendInput((uint)inputs.Length, inputs, Marshal.SizeOf(typeof(INPUT)));
    }

    public static void SendText(string text)
    {
        foreach (char c in text)
        {
            ushort keyCode = (ushort)KeyCodeFromChar(c);
            SendKey(keyCode);
        }
    }

    private static ushort KeyCodeFromChar(char c)
    {
        // Map characters to virtual key codes
        switch (c)
        {
            case 'a': return 0x41;
            case 'b': return 0x42;
            case 'c': return 0x43;
            case 'd': return 0x44;
            case 'e': return 0x45;
            case 'f': return 0x46;
            case 'g': return 0x47;
            case 'h': return 0x48;
            case 'i': return 0x49;
            case 'j': return 0x4A;
            case 'k': return 0x4B;
            case 'l': return 0x4C;
            case 'm': return 0x4D;
            case 'n': return 0x4E;
            case 'o': return 0x4F;
            case 'p': return 0x50;
            case 'q': return 0x51;
            case 'r': return 0x52;
            case 's': return 0x53;
            case 't': return 0x54;
            case 'u': return 0x55;
            case 'v': return 0x56;
            case 'w': return 0x57;
            case 'x': return 0x58;
            case 'y': return 0x59;
            case 'z': return 0x5A;
            default: return 0x00; // Default or unsupported character
        }
    }
}
