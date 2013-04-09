using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedCompletedOrderLineDto {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public AptifriedProductDto Product { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}