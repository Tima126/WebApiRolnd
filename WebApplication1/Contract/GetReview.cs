namespace WebApplication1.Contract
{
    public class GetReview
    {
        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public int? FlightId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }

    }
}
