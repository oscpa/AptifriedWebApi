using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedWebNotificationDto {
		public int Id { get; set; }
		public DateTime DateCreated { get; set; }
		public String Name { get; set; }
		public String Description { get; set; }
	}
}