using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingMedia {

        public virtual int Id { get; set; }
        public virtual string MediaFileKey { get; set; }
        public virtual string IframeCode { get; set; }
    }
}
