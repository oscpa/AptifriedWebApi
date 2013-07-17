#region using

using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedProduct
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }

        public virtual string WhoShouldPurchase { get; set; }
        public virtual string Summary { get; set; }
        public virtual string AdditionalInformation { get; set; }

        public virtual IList<AptifriedProductObjective> Objectives { get; set; }

        public virtual IList<AptifriedProductPrice> Prices { get; set; }
        public virtual AptifriedProductType Type { get; set; }

        public virtual bool IsSold { get; set; }

        public virtual bool WebEnabled { get; set; }
    }
}