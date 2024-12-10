namespace webapirold.Contract
{
    public class CreateChangesHistory
    {
        public int ChangeId { get; set; }
        public int? UserId { get; set; }
        public string TableName { get; set; } = null!;
        public int RecordId { get; set; }
        public string ChangeType { get; set; } = null!;
        public DateTime ChangeDate { get; set; }
    }
}
