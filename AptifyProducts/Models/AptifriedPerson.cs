using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AptifyWebApi.Models {
    public class AptifriedPerson {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual AptifriedAddress HomeAddress { get; set; }
        public virtual AptifriedAddress BusinessAddress { get; set; }
        public virtual AptifriedMemberType MemberType { get; set; }

    }
}