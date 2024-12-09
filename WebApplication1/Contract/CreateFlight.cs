using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateFlight
    {

       
        public string FlightNumber { get; set; } = null!;
        public int? DepartureAirportId { get; set; }
        public int? ArrivalAirportId { get; set; }


    }
}
