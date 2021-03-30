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

        public static string FileLocation
        {
            get {
                Initialize();
                return data.FileLocation;
            }
        }

        public static string FileNamePattern
        {
            get {
                Initialize();
                return data.FileNamePattern;
            }
        }

        public static string ArchiveLocation
        {
            get {
                Initialize();
                return data.ArchiveLocation;
            }
        }

        [Serializable]
        private class ConfigData
        {
            public string FileLocation { get; set; }
            public string FileNamePattern { get; set; }
            public string ArchiveLocation { get; set; }
        }
    }
}
