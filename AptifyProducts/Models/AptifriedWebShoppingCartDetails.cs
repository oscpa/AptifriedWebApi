﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebShoppingCartDetails {
        public virtual int Id { get; set; }
        public virtual int WebShoppingCartId { get; set; }
        public virtual int RegistrantId { get; set; }
        public virtual int ProductId { get; set; }
    }
}