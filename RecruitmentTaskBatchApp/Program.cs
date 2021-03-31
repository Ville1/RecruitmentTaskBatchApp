using RecruitmentTaskBatchApp.Data;
using RecruitmentTaskBatchApp.Data.Repository;
using RecruitmentTaskBatchApp.Utils;
using System.Collections.Generic;

namespace RecruitmentTaskBatchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Repository.Emails.GetAll();

            List<FileData> data = FileManager.Read();
            foreach (FileData fileData in data) {
            }
        }
    }
}
