#region using

using System.ComponentModel;
using AptifyWebApi.Helpers;

#endregion

namespace AptifyWebApi.Models.Shared
{
    public class Enums
    {
        public enum MeetingType
        {
            //[EnumHelper.MeetingTypeCategoryAttr(MeetingTypeCategory.InPerson)] Standard = 1,
            Session = 8,
        }

        public enum EducationCategoriesActiveNotInUi
        {
            Mu = 5,
            Na = 6,
        }

        public enum MeetingTypeGroup
        {
            [Description("InPerson")] InPerson = 4,
            [Description("OnLine")] Online = 2,
            [Description("Self-Study")] SelfStudy = 3
        }
    }

    public class Constants
    {
        //Column as bit in database would be ideal.
        public static string EducationCategoryStatusActive = "Active";
        public static string EducationCategoryStatusInactive = "Inactive";
    }
    }