using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedShoppingCartSubmitRequestDto {
        public int SavedShoppingCartId { get; set; }
        public string CreditCartNumber { get; set; }
        public int CardExpirationMonth { get; set; }
        public int CardExpirationYear { get; set; }
        public string CardSvn { get; set; }
    }
}