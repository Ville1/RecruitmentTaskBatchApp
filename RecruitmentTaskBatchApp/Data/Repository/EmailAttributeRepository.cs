using RecruitmentTaskBatchApp.Data.DB;
using RecruitmentTaskBatchApp.Data.DB.Model;
using RecruitmentTaskBatchApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTaskBatchApp.Data.Repository
{
    public class EmailAttributeRepository
    {
        public List<EmailAttribute> GetAll()
        {
            try {
                List<EmailAttribute> data = null;
                using (DatabaseContext db = new DatabaseContext()) {
                    data = db.EmailAttributes.ToList();
                }
                return data;
            } catch (Exception exception) {
                Logger.Log(exception);
                throw exception;
            }
        }

        public EmailAttribute Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Save(EmailAttribute data)
        {
            using (DatabaseContext db = new DatabaseContext()) {
                if (data.Id == 0 || db.EmailAttributes.FirstOrDefault(x => x.Id == data.Id) == null) {
                    db.EmailAttributes.Add(data);
                }
                db.SaveChanges();
            }
        }
    }
}
