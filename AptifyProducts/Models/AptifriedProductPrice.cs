namespace AptifyWebApi.Models
{
    public class AptifriedProductPrice
    {
        public virtual int Id { get; set; }
        public virtual int ProductId { get; set; }
        public virtual string Name { get; set; }
        public virtual AptifriedMemberType MemberType { get; set; }
        public virtual decimal Price { get; set; }
    }
}