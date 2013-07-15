namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedWebShoppingCartDetails
    {
        public virtual int Id { get; set; }
        public virtual int WebShoppingCartId { get; set; }
        public virtual int RegistrantId { get; set; }
        public virtual int ProductId { get; set; }
        public virtual AptifriedCampaign Campaign { get; set; }
    }
}