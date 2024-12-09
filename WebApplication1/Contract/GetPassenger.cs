namespace WebApplication1.Contract
{
    public class GetPassenger
    {
        public int PassengerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;

    }
}
