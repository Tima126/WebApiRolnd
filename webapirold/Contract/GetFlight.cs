namespace WebApplication1.Contract
{
    public class GetFlight
    {

        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public int? DepartureAirportId { get; set; }
        public int? ArrivalAirportId { get; set; }

    }
}
