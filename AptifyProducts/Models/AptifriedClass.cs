#region using

using System;
using System.Collections.Generic;

#endregion

namespace AptifyWebApi.Models.Aptifried
{
    public class AptifriedClass
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        public virtual AptifriedCompany Location { get; set; }
        public virtual AptifriedProduct Product { get; set; }
        public virtual AptifriedCourse Course { get; set; }
        public virtual IEnumerable<AptifriedCredit> Credits { get; set; }
    }
}