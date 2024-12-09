namespace WebApplication1.Contract
{
    public class GetBaggage
    {

        public int BaggageId { get; set; }
        public int? PassengerId { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; } = null!;

    }
}
