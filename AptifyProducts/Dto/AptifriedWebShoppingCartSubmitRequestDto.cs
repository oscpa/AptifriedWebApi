namespace AptifyWebApi.Dto
{
    public class AptifriedWebShoppingCartSubmitRequestDto
    {
        public int SavedShoppingCartId { get; set; }
        public int PaymentTypeId { get; set; }
        public string PaymentSource { get; set; }
        public string CardNumber { get; set; }
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public string CardSvn { get; set; }
    }
}