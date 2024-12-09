namespace WebApplication1.Contract
{
    public class GetChangeHistory
    {

        public int ChangeId { get; set; }
        public int? UserId { get; set; }
        public string TableName { get; set; } = null!;
        public int RecordId { get; set; }
        public string ChangeType { get; set; } = null!;

    }
}
