﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AptifyWebApi.Models {
    public class AptifriedTimeSpanProduct {
        public virtual int Id { get; set; }
        public virtual AptifriedProduct Product { get; set; }
    }
}
