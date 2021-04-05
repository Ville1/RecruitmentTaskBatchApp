﻿using Newtonsoft.Json;
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

        public static string DBConnectionString
        {
            get {
                Initialize();
                return data.DBConnectionString;
            }
        }

        public static bool ArchiveOldFiles
        {
            get {
                Initialize();
                return data.ArchiveOldFiles;
            }
        }

        public static string SendGridAPIKey
        {
            get {
                Initialize();
                return data.SendGridAPIKey;
            }
        }

        public static string SendGridEmail
        {
            get {
                Initialize();
                return data.SendGridEmail;
            }
        }

        public static string SendGridEmailName
        {
            get {
                Initialize();
                return data.SendGridEmailName;
            }
        }

        public static string SendGridSubject
        {
            get {
                Initialize();
                return data.SendGridSubject;
            }
        }

        [Serializable]
        private class ConfigData
        {
            public string FileLocation { get; set; }
            public string FileNamePattern { get; set; }
            public string ArchiveLocation { get; set; }
            public string DBConnectionString { get; set; }
            public bool ArchiveOldFiles { get; set; }
            public string SendGridAPIKey { get; set; }
            public string SendGridEmail { get; set; }
            public string SendGridEmailName { get; set; }
            public string SendGridSubject { get; set; }
        }
    }
}
