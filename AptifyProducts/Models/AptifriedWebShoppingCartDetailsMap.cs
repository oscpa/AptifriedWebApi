using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedWebShoppingCartDetailsMap : ClassMap<AptifriedWebShoppingCartDetails> {
        public AptifriedWebShoppingCartDetailsMap() {
            Table("vwWebShoppingCartDetails");
            Id(x => x.Id);
            Map(x => x.WebShoppingCartId);
            Map(x => x.ProductId);
            Map(x => x.RegistrantId);
			References(x => x.Campaign).Column("CampaignID");
            ReadOnly();
        }
    }
}