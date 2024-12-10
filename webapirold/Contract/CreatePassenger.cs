namespace webapirold.Contract
{
    public class CreatePassenger
    {
        public int PassengerId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

    }
}
