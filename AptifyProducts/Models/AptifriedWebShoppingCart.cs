﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebShoppingCart {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual AptifriedWebShoppingCartType Type { get; set; }
        public virtual AptifriedWebUser WebUser { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime DateUpdated { get; set; }
        public virtual IList<AptifriedWebShoppingCartDetails> Lines { get; set; }
        public virtual int OrderId { get; set; }
    }
}