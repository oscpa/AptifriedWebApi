﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedWebUserDto {
        public string UserName { get; set; }
        public string Password { get; set; }
		public int LinkId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
    }
}