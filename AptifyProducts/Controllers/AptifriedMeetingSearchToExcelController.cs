using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using AptifyWebApi.Dto;
using AptifyWebApi.Helpers;

namespace AptifyWebApi.Controllers
{
    //UI calls to service need refactored
    public class AptifriedMeetingSearchToExcelController
    {
        [System.Web.Http.AcceptVerbs("GET")]
        [HttpGet]
        public void ExportToExcel(AptifriedMeetingSearchResultDto resultsDto)
        {
            var excelBook = resultsDto.ToExcel();

            var fName = string.Format("OSCPA_Search_Export_{0}.xls", DateTime.Now);

            HttpContext.Current.Response.BinaryWrite(excelBook.GetAsByteArray());
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;  " + fName);
        }
    }

}