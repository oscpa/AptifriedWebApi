﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;

namespace AptifyWebApi.Models {
	public class AptifriedWebNotificationMap : ClassMap<AptifriedWebNotification> {
		public AptifriedWebNotificationMap() {
			Table("vwWebNotifications");
			Id(x => x.Id);
			Map(x => x.DateCreated);
			Map(x => x.Name);
			Map(x => x.Description);
		}
	}
}