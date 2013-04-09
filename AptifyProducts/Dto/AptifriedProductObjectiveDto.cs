using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedProductObjectiveDto {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Sequence { get; set; }
        public string Objective { get; set; }
    }
}