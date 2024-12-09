using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateNotification
    {
        
        public int? UserId { get; set; }
        public string Message { get; set; } = null!;


    }
}
