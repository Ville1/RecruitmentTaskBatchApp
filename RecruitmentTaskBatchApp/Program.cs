using RecruitmentTaskBatchApp.Data;
using RecruitmentTaskBatchApp.Utils;
using System;
using System.Collections.Generic;

namespace RecruitmentTaskBatchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileData> data = FileManager.Read();
        }
    }
}
