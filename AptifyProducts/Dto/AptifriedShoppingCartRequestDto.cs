using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedShoppingCartRequestDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public AptifriedSavedShoppingCartTypeDto Type { get; set; }
        public IList<AptifriedShoppingCartProductRequestDto> Products { get; set; }
    }
}