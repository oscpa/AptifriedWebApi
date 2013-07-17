#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedClassFilterDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<AptifriedFilterCreditDto> Credits { get; set; }
        public AptifriedAddressDto Address { get; set; }
    }
}