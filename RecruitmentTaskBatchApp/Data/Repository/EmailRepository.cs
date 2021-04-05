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
                    foreach(EmailAttribute attribute in Repository.EmailAttributes.GetAll()) {
                        data.First(x => x.EmailKey == attribute.EmailKey).Attributes.Add(Repository.Attributes.Get(attribute.AttributeId));
                    }
                }
                return data;
            } catch (Exception exception) {
                Logger.Log(exception);
                throw exception;
            }
        }

        public EmailModel Get(string key)
        {
            return GetAll().FirstOrDefault(x => x.EmailKey == key);
        }

        public void Save(EmailModel data)
        {
            using (DatabaseContext db = new DatabaseContext()) {
                if(db.Emails.FirstOrDefault(x => x.EmailKey == data.EmailKey) == null) {
                    db.Emails.Add(data);
                }
                db.SaveChanges();
            }
        }
    }
}
