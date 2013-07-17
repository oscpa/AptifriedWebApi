#region using

using System.Collections.Generic;
using AptifyWebApi.Attributes;

#endregion

namespace AptifyWebApi.Dto
{
    [AptifriedEntity(Name = "Products")]
    public class AptifriedProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public string WhoShouldPurchase { get; set; }
        public string Summary { get; set; }
        public string AdditionalInformation { get; set; }

        public virtual IList<AptifriedProductObjectiveDto> Objectives { get; set; }

        public IList<AptifriedProductPriceDto> Prices { get; set; }
        public AptifriedProductTypeDto Type { get; set; }

        public bool IsSold { get; set; }

        public bool WebEnabled { get; set; }
    }
}