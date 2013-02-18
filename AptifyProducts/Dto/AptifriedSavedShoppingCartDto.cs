using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedSavedShoppingCartDto {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string XmlData { get; set; }
        public AptifriedOrderDto Order { get; set; }
        public int OrderId { get; set; }

    }
}