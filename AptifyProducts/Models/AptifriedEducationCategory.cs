using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AptifyWebApi.Models {
    public class AptifriedEducationCategory {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
		public virtual string Status { get; set; }
    }
}
