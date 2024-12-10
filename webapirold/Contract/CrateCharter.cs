namespace webapirold.Contract
{
    public class CrateCharter
    {
        public int CharterId { get; set; }
        public int? FlightId { get; set; }
        public string CharterCompany { get; set; } = null!;
    }
}
