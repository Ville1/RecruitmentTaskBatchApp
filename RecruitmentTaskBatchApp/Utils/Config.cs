using Newtonsoft.Json;
using System;
using System.IO;

namespace RecruitmentTaskBatchApp.Utils
{
    public class Config
    {
        private static bool initialized;
        private static ConfigData data;

        private static void Initialize()
        {
            if (initialized) {
                return;
            }
            initialized = true;
            try {
                data = JsonConvert.DeserializeObject<ConfigData>(File.ReadAllText("./config.json"));
            } catch(Exception exception) {
                Logger.Log(exception);
                throw exception;
            }
        }

        public static string SaveLocation
        {
            get {
                Initialize();
                return data.SaveLocation;
            }
        }

        public static string SaveFileName
        {
            get {
                Initialize();
                return data.SaveFileName;
            }
        }

        [Serializable]
        private class ConfigData
        {
            public string SaveLocation { get; set; }
            public string SaveFileName { get; set; }
        }
    }
}
