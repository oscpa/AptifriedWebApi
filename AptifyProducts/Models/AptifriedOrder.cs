#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedOrder
    {
        public virtual int Id { get; set; }

        public virtual AptifriedPerson ShipToPerson { get; set; }

        public virtual AptifriedAddress ShippingAddress { get; set; }
        public virtual IEnumerable<AptifriedOrderLine> Lines { get; set; }


        public virtual DateTime OrderDate { get; set; }
        public virtual decimal SubTotal { get; set; }
        public virtual decimal ShippingTotal { get; set; }
        public virtual decimal Tax { get; set; }
        public virtual decimal GrandTotal { get; set; }
        public virtual decimal Balance { get; set; }
    }
}