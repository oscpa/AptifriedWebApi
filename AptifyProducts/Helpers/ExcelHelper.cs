using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AptifyWebApi.Dto;
using OfficeOpenXml;

namespace AptifyWebApi.Helpers
{
    public static class ExcelHelper
    {
        /*
        public static void ToExcel(this IEnumerable<AptifriedSearchResultDto> lst)
        {
            //Lack of T... refactor to reflected generics
 
            var pck = new ExcelPackage();

//            var len = lst.Select(x => x.TypeItem.Group.GroupId).Distinct();

            var ary = new ArrayList();
            
            foreach (var meeting in lst)
            {
                
            }
            var ws = pck.Workbook.Worksheets.Add("Worksheet1");
            var ws2 = pck.Workbook.Worksheets.Add("Worksheet1");
            var ws3 = pck.Workbook.Worksheets.Add("Worksheet1");



            ws.Cells["A1"].Value = "Sample 2";
            ws.Cells["A1"].Style.Font.Bold = true;


            var fName = string.Format("OSCPA_Search_Export_{0}", DateTime.Now);
       
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;  filename=Sample2.xlsx");
        }
         */
        
    
    }
}