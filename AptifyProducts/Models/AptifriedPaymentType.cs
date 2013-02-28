using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedPaymentType {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Type { get; set; }
        public virtual bool Active { get; set; }
        public virtual bool Inflow { get; set; }
    }
}