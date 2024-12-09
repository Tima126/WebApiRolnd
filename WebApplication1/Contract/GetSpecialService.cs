namespace WebApplication1.Contract
{
    public class GetSpecialService
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = null!;
        public string? Description { get; set; }

    }
}
