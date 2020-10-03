using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

namespace ScreenFixer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            string previousRefreshRate = "";
            string deviceName = "";
            bool isKeyPressed = false;

            while (true)
            {
                Thread.Sleep(50);
                if ((Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift) && Keyboard.IsKeyDown(Key.F)) && !isKeyPressed)
                {
                    isKeyPressed = true;
                    if (Screen.AllScreens.Length == 2)
                    {
                        deviceName = Screen.AllScreens[1].DeviceName;
                        previousRefreshRate = File.ReadAllText("second-screen-refresh-rate.txt");
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
                }
            }
        } 
    }
}
