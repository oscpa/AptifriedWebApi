#region using

using AptifyWebApi.Attributes;

#endregion

namespace AptifyWebApi.Dto
{
    [AptifriedEntity(Name = "Addresses")]
    public class AptifriedAddressDto
    {
        [AptifriedEntityField(FieldName = "ID")]
        public int Id { get; set; }

        [AptifriedEntityField(FieldName = "Line1")]
        public string Line1 { get; set; }

        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
    }
}