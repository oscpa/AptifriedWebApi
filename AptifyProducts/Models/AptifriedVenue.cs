namespace AptifyWebApi.Models
{
    public class AptifriedVenue
    {
        public virtual int Id { get; set; }
        public virtual AptifriedVenue Parent { get; set; }
        public virtual string Name { get; set; }
        public virtual AptifriedAddress Address { get; set; }
    }
}