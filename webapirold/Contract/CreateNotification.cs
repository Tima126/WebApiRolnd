namespace webapirold.Contract
{
    public class CreateNotification
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime NotificationDate { get; set; }
        public bool? IsRead { get; set; }

    }
}
