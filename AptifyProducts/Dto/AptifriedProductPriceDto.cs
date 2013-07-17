#region using

using AptifyWebApi.Attributes;

#endregion

namespace AptifyWebApi.Dto
{
    [AptifriedEntity(Name = "ProductPrices")]
    public class AptifriedProductPriceDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public AptifriedMemberTypeDto MemberType { get; set; }
        public decimal Price { get; set; }
    }
}