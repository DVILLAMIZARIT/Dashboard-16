using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Web.Mvc;

namespace WebUI.Helpers.Notifications
{
    public static class NotificationExtensions
    {
        private const String NotificationsTempDataKey = @"Dashboard.Notifications";\

        public static void AddNotification(this Controller controller, NotificationType type, String message, String title = null, Boolean isBlock = false)
        {
            Contract.Requires<ArgumentNullException>(!String.IsNullOrEmpty(message));

            List<Notification> notifications = controller.TempData[NotificationsTempDataKey] as List<Notification>;
            if (notifications == null)
            {
                notifications = new List<Notification>();
                controller.TempData[NotificationsTempDataKey] = notifications;
            }
            Notification notification = new Notification(message, type)
            {
                IsBlock = isBlock
            };
            if (!String.IsNullOrEmpty(title))
            {
                notification.Title = title;
            }
            notifications.Add(notification);
        }
    }
}