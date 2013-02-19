using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedShoppingCartAddRequestDto {
        public int Id { get; set; }
        public IList<AptifriedShoppingCartProductAddRequestDto> Products { get; set; }
    }
}