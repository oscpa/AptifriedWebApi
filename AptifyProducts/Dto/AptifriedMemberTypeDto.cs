using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedMemberTypeDto {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsMember { get; set; }
    }
}