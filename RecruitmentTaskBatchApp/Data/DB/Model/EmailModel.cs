using System;
using System.ComponentModel.DataAnnotations;

namespace RecruitmentTaskBatchApp.Data.DB.Model
{
    public class EmailModel
    {
        [Key]
        public string EmailKey { get; set; }
        public string Email { get; set; }
        public DateTime? Created { get; set; }
    }
}
