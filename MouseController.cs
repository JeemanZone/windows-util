using System;
using System.Runtime.InteropServices;
using System.Threading;

public class MouseController
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetCursorPos(int X, int Y);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    // Constants for mouse events
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;

    // Structure to store the cursor position
    public struct POINT
    {
        public int X;
        public int Y;
    }

    public static void MoveMouse(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public static void LeftClick()
    {
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
    }

    public static void RightClick()
    {
        mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
        mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
    }

    // Method to get the current mouse position
    public static POINT GetMousePosition()
    {
        POINT currentMousePoint;
        GetCursorPos(out currentMousePoint);
        return currentMousePoint;
    }

    // Method to move the mouse smoothly over a set duration
    public static void MoveMouseSmooth(int targetX, int targetY, int durationMs)
    {
        // Get the current position of the mouse
        POINT currentPos = GetMousePosition();

        // Calculate the total distance to move
        int distanceX = targetX - currentPos.X;
        int distanceY = targetY - currentPos.Y;

        // Divide the movement into small steps
        int steps = 100;  // Number of steps for smooth movement
        double stepDuration = (double)durationMs / steps;  // Time per step

        for (int i = 1; i <= steps; i++)
        {
            // Calculate the next position
            int newX = currentPos.X + (int)(distanceX * ((double)i / steps));
            int newY = currentPos.Y + (int)(distanceY * ((double)i / steps));

            // Move the mouse to the new position
            SetCursorPos(newX, newY);

            // Wait for the step duration to create a smooth movement effect
            Thread.Sleep((int)stepDuration);
        }
    }
}
