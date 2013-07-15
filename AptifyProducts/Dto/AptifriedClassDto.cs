#region using

using System;
using System.Collections.Generic;
using AptifyWebApi.Attributes;

#endregion

namespace AptifyWebApi.Models.Dto
{
    [AptifriedEntity(Name = "Classes")]
    public class AptifriedClassDto
    {
        [AptifriedEntityField(FieldName = "ID")]
        public int Id { get; set; }

        [AptifriedEntityField(FieldName = "Name")]
        public string Name { get; set; }

        [AptifriedEntityField(FieldName = "StartDate")]
        public DateTime StartDate { get; set; }

        [AptifriedEntityField(FieldName = "EndDate")]
        public DateTime EndDate { get; set; }


        public AptifriedCompanyDto Location { get; set; }
        public AptifriedProductDto Product { get; set; }
        public AptifriedCourseDto Course { get; set; }
        public IEnumerable<AptifriedCreditDto> Credits { get; set; }
    }
}