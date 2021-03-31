using RecruitmentTaskBatchApp.Data.DB;
using RecruitmentTaskBatchApp.Data.DB.Model;
using RecruitmentTaskBatchApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTaskBatchApp.Data.Repository
{
    public class EmailRepository
    {
        public List<EmailModel> GetAll()
        {
            try {
                List<EmailModel> data = null;
                using (DatabaseContext db = new DatabaseContext()) {
                    data = db.Emails.ToList();
                }
                return data;
            } catch (Exception exception) {
                Logger.Log(exception);
                throw exception;
            }
        }
    }
}
