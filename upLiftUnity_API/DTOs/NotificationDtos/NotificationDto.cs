namespace upLiftUnity_API.DTOs.NotificationDtos
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string NotificationEvent { get; set; }

        public bool IsAcknowledged { get; set; }

        public string RelativeNotifiedDateAndTime { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }


    public interface IClientNotificationHub
    {
        Task ClientReceiveNotification(NotificationDto notification);
    }

}
