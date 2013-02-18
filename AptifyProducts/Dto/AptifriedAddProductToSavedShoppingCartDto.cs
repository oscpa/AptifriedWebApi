using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedAddProductToSavedShoppingCartDto {
        public int Id { get; set; }
        public IEnumerable<int> Products { get; set; }
    }
}