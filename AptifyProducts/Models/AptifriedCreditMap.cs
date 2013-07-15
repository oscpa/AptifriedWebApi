#region using

using FluentNHibernate.Mapping;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedCreditMap : ClassMap<AptifriedCredit>
    {
        public AptifriedCreditMap()
        {
            Table("vwClassCPECredits");
            Id(m => m.Id);
            Map(x => x.Name).Column("CPETypeIDName");
            Map(x => x.Code).Column("CPECodeID_Code");
            Map(x => x.Amount).Column("Credits");
        }
    }
}