#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedOrderDto
    {
        public AptifriedPersonDto ShipToPerson { get; set; }

        public AptifriedAddressDto ShippingAddress { get; set; }
        public IEnumerable<AptifriedOrderLineDto> Lines { get; set; }

        public int Id { get; set; }

        public decimal SubTotal { get; set; }
        public decimal ShippingTotal { get; set; }
        public decimal Tax { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Balance { get; set; }

        public string AptifyLastError { get; set; }
    }
}