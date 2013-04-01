using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Attributes;

namespace AptifyWebApi.Dto {

    [AptifriedEntity(Name="Products")]    
    public class AptifriedProductDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public IList<AptifriedProductPriceDto> Prices { get; set; }
        public AptifriedProductTypeDto Type { get; set; }
    }
}