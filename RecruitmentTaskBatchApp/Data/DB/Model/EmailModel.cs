using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentTaskBatchApp.Data.DB.Model
{
    public class EmailModel
    {
        [Key]
        public string EmailKey { get; set; }
        public string Email { get; set; }
        public DateTime? Created { get; set; }

        [NotMapped]
        public List<AttributeData> Attributes { get; set; }

        public EmailModel()
        {
            Attributes = new List<AttributeData>();
        }
    }
}
