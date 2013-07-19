#region using

using System.ComponentModel;
using AptifyWebApi.Helpers;

#endregion

namespace AptifyWebApi.Models.Shared
{
    public class EnumsAndConstantsToAvoidDatabaseChanges
    {
        public enum MeetingType
        {
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Standard = 1,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.Online)] Webcast = 2,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Conference = 3,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] OnSite = 4,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson), EnumHelper.MinDays(7)] SelfStudy = 5,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.SelfStudy)] Session = 6,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Webinar = 7,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.Online)] Seminar = 8,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Networking = 9,
            [EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Other = 10
        }

        public enum MeetingTypeCategory
        {
            [Description("InPerson")] InPerson = 1,
            [Description("OnLine")] Online = 2,
            [Description("Self-Study")] SelfStudy = 3
        }

        //Column as bit in database would be ideal.
        public static string EducationCategoryStatusActive = "Active";
        public static string EducationCategoryStatusInactive = "Inactive";
    }
    }