using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedProductPersonNote {
		public virtual int Id { get; set; }
		public virtual AptifriedProduct Product { get; set; }
		public virtual AptifriedPerson Person { get; set; }
		public virtual String Body { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual DateTime DateUpdated { get; set; }
        public virtual Boolean IsActive { get; set; }
	}
}