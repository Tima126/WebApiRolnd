namespace WebApplication1.Contract
{
    public class GetCharter
    {

        public int CharterId { get; set; }
        public int? FlightId { get; set; }
        public string CharterCompany { get; set; } = null!;

    }
}
