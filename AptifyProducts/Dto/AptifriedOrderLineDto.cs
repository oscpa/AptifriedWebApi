﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedOrderLineDto {
        public AptifriedProductDto Product { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal Extended { get; set; }
    }
}