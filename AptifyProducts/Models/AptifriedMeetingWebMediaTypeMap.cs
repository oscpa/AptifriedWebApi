﻿using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedMeetingWebMediaTypeMap : ClassMap<AptifriedMeetingWebMediaType> {
        public AptifriedMeetingWebMediaTypeMap() {
            Table("vwWebMediaTypes");
            Id(x => x.Id);
            Map(x => x.Name);
            ReadOnly();
        }
    }
}