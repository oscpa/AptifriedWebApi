using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Iesi.Collections;

namespace AptifyWebApi.Models
{
    public class AptifriedZipCode
    {
        public virtual int Id { get; set; }
        public virtual decimal Latitude { get; set; }
        public virtual decimal Longitude { get; set;} 
        public virtual string PostalCode { get; set; }
        public virtual string Miles { get; set; }
    
    }
}