namespace AptifyWebApi.Models
{
    public class AptifriedMemberClassificationType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual string DefaultType { get; set; }
        public virtual string OldID { get; set; }
    }
}