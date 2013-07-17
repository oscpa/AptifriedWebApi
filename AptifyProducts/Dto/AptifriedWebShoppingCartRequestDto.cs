#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedWebShoppingCartRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AptifriedWebShoppingCartTypeDto ShoppingCartType { get; set; }
        public IList<AptifriedWebShoppingCartProductRequestDto> Products { get; set; }
    }
}