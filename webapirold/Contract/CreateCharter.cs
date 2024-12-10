namespace webapirold.Contract
{
    public class CreateCharter
    {
        public int CharterId { get; set; }
        public int? FlightId { get; set; }
        public string CharterCompany { get; set; } = null!;
    }
}
