using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
namespace ScreenFixer
{
    static class Program
    {
        static void Main()
        {
            string previousRefreshRate = File.ReadAllText("second-screen-refresh-rate.txt");
            string deviceName = "";
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
    } 
}
