namespace WebApplication1.Contract
{
    public class GetAirport
    {

        public int AirportId { get; set; }
        public string AirportCode { get; set; } = null!;
        public string AirportName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

    }
}
