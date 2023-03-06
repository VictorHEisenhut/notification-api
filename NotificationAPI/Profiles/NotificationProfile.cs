using AutoMapper;
using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;

namespace NotificationAPI.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<Notification, ReadNotificationDto>();
            CreateMap<ReadNotificationDto, Notification>();

            CreateMap<Notification, CreateNotificationDto>();
            CreateMap<CreateNotificationDto, Notification>();

        }
    }
}
