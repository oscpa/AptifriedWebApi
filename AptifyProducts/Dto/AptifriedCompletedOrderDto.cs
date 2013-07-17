#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedCompletedOrderDto
    {
        public int Id { get; set; }

        public AptifriedPersonDto ShipToPerson { get; set; }

        public AptifriedAddressDto ShippingAddress { get; set; }
        public IEnumerable<AptifriedCompletedOrderLineDto> Lines { get; set; }

        public DateTime OrderDate { get; set; }
        public decimal SubTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Balance { get; set; }
    }
}