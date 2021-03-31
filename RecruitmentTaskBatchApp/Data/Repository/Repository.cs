namespace RecruitmentTaskBatchApp.Data.Repository
{
    public class Repository
    {
        public static EmailRepository Emails { get { return new EmailRepository(); } }
        public static AttributeRepository Attributes { get { return new AttributeRepository(); } }
        public static EmailAttributeRepository EmailAttributes { get { return new EmailAttributeRepository(); } }
    }
}
