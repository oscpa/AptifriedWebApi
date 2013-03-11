using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AptifyWebApi.Models {
    public class AptifriedExamQuestion {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Question { get; set; }
        public virtual string Answer { get; set; }
        

    }
}
