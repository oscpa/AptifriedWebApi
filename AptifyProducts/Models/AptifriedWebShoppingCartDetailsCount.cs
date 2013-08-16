using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
	public class AptifriedWebShoppingCartDetailsCount {
		public virtual int WebUserId { get; set; }
		public virtual AptifriedWebUser WebUser { get; set; }
		public virtual int Items { get; set; }
	}
}