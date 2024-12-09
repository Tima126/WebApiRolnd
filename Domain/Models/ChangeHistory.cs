using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class ChangeHistory
    {
        public int ChangeId { get; set; }
        public int? UserId { get; set; }
        public string TableName { get; set; } = null!;
        public int RecordId { get; set; }
        public string ChangeType { get; set; } = null!;
        public DateTime ChangeDate { get; set; }

        public virtual User? User { get; set; }
    }
}
