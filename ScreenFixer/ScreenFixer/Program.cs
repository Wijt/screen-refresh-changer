using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;
using System.Runtime.InteropServices;

namespace ScreenFixer
{
    static class Program
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        [STAThread]
        static void Main()
        {
            string previousRefreshRate = File.ReadAllText("second-screen-refresh-rate.txt");
            string deviceName = "";
            bool isKeyPressed = false;
            var handle = GetConsoleWindow();

            while (true)
            {
                Thread.Sleep(50);
                if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.F)) && !isKeyPressed)
                {
                    ShowWindow(handle, SW_SHOW);
                    isKeyPressed = true;
                    if (Screen.AllScreens.Length == 2)
                    {
                        deviceName = Screen.AllScreens[1].DeviceName;
                        Console.WriteLine("Second device name: " + deviceName + ", Refreshrate: " + previousRefreshRate);
                    }

                    previousRefreshRate = previousRefreshRate == "60" ? "50" : "60";
                    Console.WriteLine("New refreshrate: " + previousRefreshRate);

                    var StartInfo = new ProcessStartInfo
                    {
                        FileName = @"C:\Program Files (x86)\12noon Display Changer\dc64.exe",
                        Arguments = " -monitor=\"" + deviceName + "\" -width=1920 -height=1080 -refresh=" + previousRefreshRate + " -depth=32",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = false,
                        WorkingDirectory = @"C:\Program Files (x86)\12noon Display Changer\"
                    };
                    using (Process process = Process.Start(StartInfo))
                    {
                        process.WaitForExit();
                    }
                    Console.WriteLine("-------------------------------");
                    File.WriteAllText("second-screen-refresh-rate.txt", previousRefreshRate);
                }
                if ((Keyboard.IsKeyUp(Key.LeftCtrl) && Keyboard.IsKeyUp(Key.LeftShift) && Keyboard.IsKeyUp(Key.F)) && isKeyPressed)
                {
                    isKeyPressed = false;
                    ShowWindow(handle, SW_HIDE);
                }
            }
        } 
    }
}
