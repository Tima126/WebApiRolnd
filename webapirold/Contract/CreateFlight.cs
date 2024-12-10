namespace webapirold.Contract
{
    public class CreateFlight
    {
        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public int? DepartureAirportId { get; set; }
        public int? ArrivalAirportId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
