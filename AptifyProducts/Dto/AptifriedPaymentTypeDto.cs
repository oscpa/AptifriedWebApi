using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedPaymentTypeDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
        public bool Inflow { get; set; }
    }
}