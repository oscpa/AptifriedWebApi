namespace AptifyWebApi.Dto
{
    public class AptifriedVenueDto
    {
        public int Id { get; set; }
        public AptifriedVenueDto Parent { get; set; }
        public string Name { get; set; }
        public AptifriedAddressDto Address { get; set; }
    }
}