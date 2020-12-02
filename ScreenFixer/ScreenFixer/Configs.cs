using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ScreenFixer
{
    class Configs
    {
        public string depth, rRate1, rRate2, width, height;
        public bool debugMode;
        public Configs(string configFileName = "configs.json")
        {
            Configs.Update(this, configFileName);
        }

        static void Update(Configs configs, string fileName)
        {
            string configJSON = File.ReadAllText(fileName);
            JObject configsInFile = JObject.Parse(configJSON);
            configs.width = (string)configsInFile["ScreenSize"]["Width"];
            configs.height = (string)configsInFile["ScreenSize"]["Height"];
            configs.depth = (string)configsInFile["Depth"];
            configs.rRate1 = (string)configsInFile["RefreshRateOne"];
            configs.rRate2 = (string)configsInFile["RefreshRateTwo"];
            configs.debugMode = (bool)configsInFile["DebugMode"];
        }
        public override string ToString()
        {
            return width + "x" + height + " " + depth + " " + rRate1 + " " + rRate2;
        }
    }
}
