using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateBooking
    {

       
        public int? UserId { get; set; }
        public int? FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = null!;


    }
}
