using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedWebNotification {
		public virtual int Id { get; set; }
		public virtual DateTime DateCreated { get; set; }
		public virtual String Name { get; set; }
		public virtual String Description { get; set; }
	}
}