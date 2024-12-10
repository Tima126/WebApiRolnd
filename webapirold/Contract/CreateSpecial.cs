namespace webapirold.Contract
{
    public class CreateSpecial
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
