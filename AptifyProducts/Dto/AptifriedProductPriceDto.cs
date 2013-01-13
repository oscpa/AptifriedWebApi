using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedProductPriceDto {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public AptifriedMemberTypeDto MemberType { get; set; }
        public decimal Price { get; set; }
    }
}