using Domain.Models;

namespace WebApplication1.Contract
{
    public class CreateChangeHistory
    {

        
        public int? UserId { get; set; }
        public string TableName { get; set; } = null!;
        public int RecordId { get; set; }
        public string ChangeType { get; set; } = null!;

    }
}
