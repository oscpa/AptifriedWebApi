namespace AptifyWebApi.Dto
{
    public class AptifriedWebShoppingCartProductRequestDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int RegistrantId { get; set; }
        public AptifriedCampaignDto Campaign { get; set; }
    }
}