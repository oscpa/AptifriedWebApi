using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifiriedClassFilterDto {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<AptifriedFilterCreditDto> Credits { get; set; }
        public AptifriedAddressDto Address { get; set; }
    }
}