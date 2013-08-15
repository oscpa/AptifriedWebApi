using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;
using NHibernate;

namespace AptifyWebApi.Controllers
{
    //UI calls to service need refactored
    public class AptifriedExcelController : AptifyEnabledApiController
    {
        public AptifriedExcelController(ISession session) : base(session)
        {
        }

        [System.Web.Http.AcceptVerbs("Post")]
        [HttpPost]
        public void Post(AptifriedMeetingSearchResultDto resultsDto)
        {
            var excelBook = resultsDto.ToExcel();

            var fName = string.Format("OSCPA_Search_Export_{0}.xls", DateTime.Now);

            HttpContext.Current.Response.BinaryWrite(excelBook.GetAsByteArray());
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  " + fName);
        }
    }

}