using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateCharter
    {

       
        public int? FlightId { get; set; }
        public string CharterCompany { get; set; } = null!;


    }
}
