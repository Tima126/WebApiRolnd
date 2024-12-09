namespace WebApplication1.Contract
{
    public class GetNotification
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; } = null!;

    }
}
