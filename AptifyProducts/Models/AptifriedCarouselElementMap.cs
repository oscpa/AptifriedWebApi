#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models
{
    public class AptifriedCarouselElementMap : ClassMap<AptifriedCarouselElement>
    {
        public AptifriedCarouselElementMap()
        {
            ReadOnly();
            Id(x => x.ProductId);
            Map(x => x.BackgroundImageUrl);
        }
    }
}