namespace AptifyWebApi.Models
{
    public class AptifriedCompany
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual AptifriedAddress Address { get; set; }
    }
}