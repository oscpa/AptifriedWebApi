using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AptifyWebApi.Models.Aptifried;

namespace AptifyWebApi.Models {
    public class AptifriedTimeSpanProduct {
        public virtual int Id { get; set; }
        public virtual AptifriedProduct Product { get; set; }
    }
}
