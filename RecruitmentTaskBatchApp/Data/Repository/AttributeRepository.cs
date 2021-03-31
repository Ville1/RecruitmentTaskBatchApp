using System;
using System.Collections.Generic;
using System.Linq;
using RecruitmentTaskBatchApp.Data.DB;
using RecruitmentTaskBatchApp.Data.DB.Model;
using RecruitmentTaskBatchApp.Utils;

namespace RecruitmentTaskBatchApp.Data.Repository
{
    public class AttributeRepository
    {
        public List<AttributeData> GetAll()
        {
            try {
                List<AttributeData> data = null;
                using (DatabaseContext db = new DatabaseContext()) {
                    data = db.Attributes.ToList();
                }
                return data;
            } catch (Exception exception) {
                Logger.Log(exception);
                throw exception;
            }
        }

        public AttributeData Get(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Save(AttributeData data)
        {
            using (DatabaseContext db = new DatabaseContext()) {
                db.Attributes.Add(data);
                db.SaveChanges();
            }
        }
    }
}
