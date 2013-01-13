using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedCompanyDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public AptifriedAddressDto Address { get; set; }
    }
}