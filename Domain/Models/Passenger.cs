using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Passenger
    {
        public Passenger()
        {
            Baggages = new HashSet<Baggage>();
            Bookings = new HashSet<Booking>();
        }

        public int PassengerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Baggage> Baggages { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
