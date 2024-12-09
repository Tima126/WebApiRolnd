using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreatePassenger
    {
        
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;

    }
}
