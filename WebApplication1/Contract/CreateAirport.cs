using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateAirport
    {

        
        public string AirportCode { get; set; } = null!;
        public string AirportName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;


    }
}
