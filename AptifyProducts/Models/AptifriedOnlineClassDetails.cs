using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedOnlineClassDetails {

        public virtual AptifriedOnlineClassDetails Parent { get; private set; }
        public virtual AptifriedClass Class { get; private set; }
        public virtual AptifriedCourse Course { get; private set; }
        public virtual AptifriedProduct Product { get; private set; }
        public virtual AptifriedProductPrice Price { get; private set; }

    }
}