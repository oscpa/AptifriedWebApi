using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AptifyWebApi.Repository {
    /// <summary>
    /// Describes a mock entity object so that we can inject somthing similar to it.
    /// </summary>
    interface IAptifyGenericEntity {
        string EntityRecordName { get; set; }
        void SetValue(string FieldName, object FieldValue);
        bool Save(ref string errorString);
    }
}
