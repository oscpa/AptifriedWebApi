using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedWebShoppingCartDetailsCountDto {
		public AptifriedWebUserDto WebUser { get; set; }
		public int Items { get; set; }
	}
}