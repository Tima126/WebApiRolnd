namespace WebApplication1.Contract
{
    public class GetBooking
    {

        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? FlightId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = null!;

    }
}
