namespace webapirold.Contract
{
    public class CreateUser
    {

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public int? RoleId { get; set; }
    }
}
