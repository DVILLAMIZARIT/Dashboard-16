using System;

namespace WebUI.Helpers.Notifications
{
    public enum NotificationType
    {
        Default,

        Error,
        Information,
        Success,
        Warning
    }

    public class Notification
    {
        public Boolean IsBlock { get; set; }
        public String Message { get; set; }
        public String Title { get; set; }
        public NotificationType Type { get; set; }

        public Notification(String message, NotificationType type = NotificationType.Default)
        {
            this.Message = message;
            this.Type = type;
        }
    }
}