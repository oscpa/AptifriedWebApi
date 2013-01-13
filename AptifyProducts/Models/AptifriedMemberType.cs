using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMemberType {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsMember { get; set; }
    }
}