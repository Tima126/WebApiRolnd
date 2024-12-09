using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Airport
    {
        public Airport()
        {
            FlightArrivalAirports = new HashSet<Flight>();
            FlightDepartureAirports = new HashSet<Flight>();
        }

        public int AirportId { get; set; }
        public string AirportCode { get; set; } = null!;
        public string AirportName { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Country { get; set; } = null!;

        public virtual ICollection<Flight> FlightArrivalAirports { get; set; }
        public virtual ICollection<Flight> FlightDepartureAirports { get; set; }
    }
}
