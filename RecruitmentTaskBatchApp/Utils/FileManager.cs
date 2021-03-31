using Newtonsoft.Json;
using RecruitmentTaskBatchApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RecruitmentTaskBatchApp.Utils
{
    public class FileManager
    {
        public static List<FileData> Read()
        {
            try {
                List<FileData> data = new List<FileData>();
                foreach(string fullFileName in Directory.GetFiles(Config.FileLocation, Config.FileNamePattern)) {
                    StringBuilder jsonStringBuilder = new StringBuilder(File.ReadAllText(fullFileName));
                    jsonStringBuilder.Remove(jsonStringBuilder.Length - 3, 3);
                    jsonStringBuilder.Insert(0, "{\"Data\": [");
                    jsonStringBuilder.Append("]}");
                    data.Add(new FileData() {
                        FullName = fullFileName,
                        Emails = JsonConvert.DeserializeObject<EmailDataWrapper>(jsonStringBuilder.ToString()).Data
                    });
                }
                return data;
            } catch(Exception exception) {
                Logger.Log(exception);
                throw exception;
            }
        }
    }
}
