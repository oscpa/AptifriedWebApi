using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedEducationUnitAggregateDto {
        public string Name { get; set; }
        public int ProductId { get; set; }
        public string Location { get; set; }
        public string CreditTypeCode { get; set; }
        public decimal Credits { get; set; }
        public string FormattedDates { get; set; }
    }
}