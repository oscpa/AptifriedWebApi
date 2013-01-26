using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedSavedShoppingCart {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual int WebUserId { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual DateTime XmlData { get; set; }
        public virtual int OrderId { get; set; }
    }
}