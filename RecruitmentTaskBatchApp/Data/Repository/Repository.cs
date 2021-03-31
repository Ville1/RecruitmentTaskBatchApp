using RecruitmentTaskBatchApp.Data.DB.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RecruitmentTaskBatchApp.Data.Repository
{
    public class Repository
    {
        public static EmailRepository Emails { get { return new EmailRepository(); } }
    }
}
