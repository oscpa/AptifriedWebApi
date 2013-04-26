using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedOrderLineMap : ClassMap<AptifriedOrderLine> {
        public AptifriedOrderLineMap() {
            Table("vwOrderDetails");
            Id(x => x.Id);
            Map(x => x.OrderId);
            Map(x => x.Price);
            Map(x => x.Discount);
            References(x => x.Product).Column("ProductID");
			References(x => x.Campaign).Column("CampaignCodeID");

            ReadOnly();

        }
    }
}