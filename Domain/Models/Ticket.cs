using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int? BookingId { get; set; }
        public string SeatNumber { get; set; } = null!;
        public string Class { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual Booking? Booking { get; set; }
    }
}
