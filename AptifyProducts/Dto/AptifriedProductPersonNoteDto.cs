using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
	public class AptifriedProductPersonNoteDto {
		public int Id { get; set; }
		public AptifriedProductDto Product { get; set; }
		public AptifriedPersonDto Person { get; set; }
		public String Body { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateUpdated { get; set; }
        public Boolean IsActive { get; set; }
	}
}