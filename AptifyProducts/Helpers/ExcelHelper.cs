using System.Linq;
using AptifyWebApi.Dto;
using OfficeOpenXml;

namespace AptifyWebApi.Helpers
{
    public static class ExcelHelper
    {
        
        public static ExcelPackage ToExcel(this AptifriedMeetingSearchResultDto results)
        {
            //Lack of T... refactor to method.invoke reflected generics
 
            var pck = new ExcelPackage();

            foreach (var worksheet in from tabResult in results.ResultList let worksheet = pck.Workbook.Worksheets.Add(tabResult[0].TypeItem.Group.Name) 
                                      from meeting in tabResult select worksheet)
            {
                //TODO: Excel: Build worksheets by meeting

                //reflect property name for heading

                worksheet.Cells["A1"].Value = "heading";
                worksheet.Cells["A1"].Style.Font.Bold = true;

                //fill cells per meeting
            }

            return pck;
        }
    
    }
}