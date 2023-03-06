﻿using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;

namespace NotificationAPI.Services
{
    public interface INotificationService
    {
        void SaveChanges();

        IEnumerable<ReadNotificationDto> GetNotifications(int skip = 0, int take = 50);
        Notification GetNotificationByID(int id);
        void CreateNotification(Notification notification);
        void DeleteNotification(int id);
    }
}
