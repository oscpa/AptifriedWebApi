#region using

using System.ComponentModel;
using AptifyWebApi.Helpers;

#endregion

namespace AptifyWebApi.Models.Shared
{
    public class EnumsAndConstants
    {
        public enum MeetingType
        {
            //[EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Standard = 1,
            Session = 8,
        }

        public enum MeetingTypeGroup
        {
            [Description("InPerson")] InPerson = 4,
            [Description("OnLine")] Online = 2,
            [Description("Self-Study")] SelfStudy = 3
        }

        //Column as bit in database would be ideal.
        public static string EducationCategoryStatusActive = "Active";
        public static string EducationCategoryStatusInactive = "Inactive";
    }
    }