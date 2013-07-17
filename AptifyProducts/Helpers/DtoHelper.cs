using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AptifyWebApi.Dto;
using AptifyWebApi.Models.Shared;
using NHibernate;

namespace AptifyWebApi.Helpers
{
    public static class DtoHelper
    {
        public static AptifriedMeetingSearchDto ConvertStringMeetingTypesToMeetingTypeObjects(this AptifriedMeetingSearchDto sParam, ISession session) 
        { 
            foreach (var mt in sParam.MeetingType)
            {
                sParam.MeetingTypesObjList.Add(session.GetMeetingTypeDtoByName(mt));
            }

            return sParam;
        } 
    }
}