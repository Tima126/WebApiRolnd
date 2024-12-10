using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Booking
    {
        public Booking()
        {
            Tickets = new HashSet<Ticket>();
            Passengers = new HashSet<Passenger>();
            Services = new HashSet<Special>();
        }

        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = null!;

        public virtual Flight? Flight { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Special> Services { get; set; }
    }
}
