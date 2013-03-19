using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedCarouselElementMap : ClassMap<AptifriedCarouselElement>{
        public AptifriedCarouselElementMap() {
            ReadOnly();
            Id(x => x.ProductId);
            Map(x => x.BackgroundImageUrl);
        }
    }
}