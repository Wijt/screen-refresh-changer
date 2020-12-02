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
            Configs configs = new Configs();

            string deviceName = "";
            if (Screen.AllScreens.Length == 2)
            {
                deviceName = Screen.AllScreens[1].DeviceName;
                Console.WriteLine("Second device name: " + deviceName + ", Refreshrate: " + previousRefreshRate);
            }

            previousRefreshRate = previousRefreshRate == configs.rRate1 ? configs.rRate2 : configs.rRate1;
            Console.WriteLine("New refreshrate: " + previousRefreshRate);

            var StartInfo = new ProcessStartInfo
            {
                FileName = configs.filePathTo12noon + "dc64.exe",
                Arguments = " -monitor=\"" + deviceName + "\" -width=" + configs.width + " -height=" + configs.height + " -refresh=" + previousRefreshRate + " -depth=" + configs.depth,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = false,
                WorkingDirectory = configs.filePathTo12noon
            };

            using (Process process = Process.Start(StartInfo))
            {
                process.WaitForExit();
            }
            File.WriteAllText("second-screen-refresh-rate.txt", previousRefreshRate);
            if (configs.debugMode)
            {
                Console.WriteLine("-------------------------------");
                Console.WriteLine(configs.ToString());
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}

