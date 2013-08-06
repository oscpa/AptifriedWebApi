﻿
 #region using

using AptifyWebApi.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#endregion

namespace AptifyWebApi.Helpers
{
    public static class EnumHelper
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof (T), false);
            return (T) attributes[0];
        }

        public static string Description(this Enum enumVal)
        {
            var attribute = enumVal.GetAttributeOfType<DescriptionAttribute>();

            return attribute == null ? String.Empty : attribute.Description;
        }

        public static int CountAll<T>()
        {
            return Enum.GetValues(typeof (T)).Length;
        }

        #region Mapping Table Needed

        public static int GetMinDays(this EnumsAndConstants.MeetingType enumVal)
        {
            var attribute = enumVal.GetAttributeOfType<MinDays>();

            return attribute.Days;
        }

        public static EnumsAndConstants.MeetingTypeGroup CategoryId(
            this EnumsAndConstants.MeetingType enumVal)
        {
            var attribute = enumVal.GetAttributeOfType<MeetingTypeCategoryAttr>();

            return attribute.MeetingTypeCategory;
        }


        public static IEnumerable<int> GetMeetingTypeIdsByCategoryDescription(string mTypeDesc)
        {
            var categoryId =
                (EnumsAndConstants.MeetingTypeGroup)
                Enum.Parse(typeof (EnumsAndConstants.MeetingTypeGroup), mTypeDesc, true);

            return
                Enum.GetValues(typeof (EnumsAndConstants.MeetingType))
                    .Cast<EnumsAndConstants.MeetingType>()
                    .Where(m => m.CategoryId() == categoryId)
                    .ToList()
                    .Cast<int>();
        }

        public static int GetMeetingTypeIdByName(string mTypeName)
        {
            var id = (EnumsAndConstants.MeetingType)
                Enum.Parse(typeof(EnumsAndConstants.MeetingType), mTypeName, true);

            return (int)id;
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class MeetingTypeCategoryAttr : Attribute
        {
            private readonly EnumsAndConstants.MeetingTypeGroup _meetingTypeCategory;

            public MeetingTypeCategoryAttr(EnumsAndConstants.MeetingTypeGroup mCat)
            {
                _meetingTypeCategory = mCat;
            }

            public EnumsAndConstants.MeetingTypeGroup MeetingTypeCategory
            {
                get { return _meetingTypeCategory; }
            }
        }

        [AttributeUsage(AttributeTargets.Field)]
        public class MinDays : Attribute
        {
            private readonly int _days;

            public MinDays(int days)
            {
                _days = days;
            }

            public int Days
            {
                get { return _days; }
            }
        }

        #endregion
    }
}