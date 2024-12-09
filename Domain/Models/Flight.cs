using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Flight
    {
        public Flight()
        {
            Bookings = new HashSet<Booking>();
            Charters = new HashSet<Charter>();
            Reviews = new HashSet<Review>();
        }

        public int FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public int? DepartureAirportId { get; set; }
        public int? ArrivalAirportId { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        public virtual Airport? ArrivalAirport { get; set; }
        public virtual Airport? DepartureAirport { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Charter> Charters { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
