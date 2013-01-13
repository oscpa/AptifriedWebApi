using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class FlattenedClassViewDto {
        public int AptifriedClassId { get; set; }
        public string AptifriedClassName { get; set; }
        public int AptifriedProductId { get; set; }
        public int AptifriedProductMemberTypeID { get; set; }
    }
}