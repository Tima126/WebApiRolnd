using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Special
    {
        public Special()
        {
            Bookings = new HashSet<Booking>();
        }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
