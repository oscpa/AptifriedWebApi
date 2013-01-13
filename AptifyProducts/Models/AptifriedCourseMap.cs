using AptifyWebApi.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedCourseMap : ClassMap<AptifriedCourse> {
        public AptifriedCourseMap() {
            Table("vwCourses");
            Id(x => x.Id);
            Map(x => x.Name);

        }
    }
}