#region using

using AptifyWebApi.Attributes;

#endregion

namespace AptifyWebApi.Dto
{
    public class AptifriedCompanyDto
    {
        [AptifriedEntityField(FieldName = "ID")]
        public int Id { get; set; }

        [AptifriedEntityField(FieldName = "Name")]
        public string Name { get; set; }

        public AptifriedAddressDto Address { get; set; }
    }
}