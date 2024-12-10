namespace webapirold.Contract
{
    public class CreateReview
    {
        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public int? FlightId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
