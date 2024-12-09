namespace WebApplication1.Contract
{
    public class GetTicket
    {
        public int TicketId { get; set; }
        public int? BookingId { get; set; }
        public string SeatNumber { get; set; } = null!;
        public string Class { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
