namespace AptifyWebApi.Models
{
    public class AptifriedOrderLine
    {
        public virtual int Id { get; set; }
        public virtual int OrderId { get; set; }

        public virtual AptifriedProduct Product { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal Discount { get; set; }
        public virtual AptifriedCampaign Campaign { get; set; }
    }
}