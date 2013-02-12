using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedSecurityKey {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string KeyValue { get; set; }
    }
}