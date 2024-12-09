using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateBaggage
    {

        
        public int? PassengerId { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; } = null!;


    }
}
