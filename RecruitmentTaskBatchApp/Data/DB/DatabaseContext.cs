using Microsoft.EntityFrameworkCore;
using RecruitmentTaskBatchApp.Data.DB.Model;
using RecruitmentTaskBatchApp.Utils;

namespace RecruitmentTaskBatchApp.Data.DB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<EmailModel> Emails { get; set; }
        public DbSet<AttributeData> Attributes { get; set; }
        public DbSet<EmailAttribute> EmailAttributes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Config.DBConnectionString);
        }
    }
}
