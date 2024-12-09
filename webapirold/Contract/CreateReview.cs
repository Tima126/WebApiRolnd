using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateReview
    {
        
        public int? UserId { get; set; }
        public int? FlightId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }


    }
}
