namespace AptifyWebApi.Models
{
    public class AptifriedCredit
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual decimal Amount { get; set; }
    }
}