using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateTicket
    {
        public int? BookingId { get; set; }
        public string SeatNumber { get; set; } = null!;
        public string Class { get; set; } = null!;
        public decimal Price { get; set; }

        
    }
}
