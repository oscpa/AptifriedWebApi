using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedWebShoppingCartDetailsCountMap : ClassMap<AptifriedWebShoppingCartDetailsCount> {
		public AptifriedWebShoppingCartDetailsCountMap() {
			Table("vwStoreWebShoppingCartDetailsCount");
			Id(x => x.WebUserId);
			References(x => x.WebUser).Column("WebUserID");
			Map(x => x.Items);
		}
	}
}