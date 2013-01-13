using AptifyWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Dto {
    public class AptifriedClassDto {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AptifriedCompanyDto Location { get; set; }
        public AptifriedProductDto Product { get; set; }
        public AptifriedCourseDto Course { get; set; }
    }
}