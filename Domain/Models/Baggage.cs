using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public partial class Baggage
    {
        public int BaggageId { get; set; }
        public int? PassengerId { get; set; }
        public decimal Weight { get; set; }
        public string Status { get; set; } = null!;

        public virtual Passenger? Passenger { get; set; }
    }
}
