using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AptifyWebApi.Dto {
    public class AptifriedTimeSpanProductDto {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public AptifriedProductDto Product { get; set; }
    }
}
