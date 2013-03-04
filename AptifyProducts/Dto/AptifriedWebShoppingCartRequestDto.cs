using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedWebShoppingCartRequestDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AptifriedWebShoppingCartTypeDto Type { get; set; }
        public IList<AptifriedWebShoppingCartProductRequestDto> Products { get; set; }
    }
}