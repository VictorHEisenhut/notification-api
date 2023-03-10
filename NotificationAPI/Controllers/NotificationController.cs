using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Data;
using NotificationAPI.Data.Dtos;
using NotificationAPI.Models;
using NotificationAPI.RabbitMqClient;
using NotificationAPI.Services;

namespace NotificationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private INotificationService _service;

        public NotificationController(IMapper mapper,  INotificationService service)
        {
            _mapper = mapper;
            _service = service;
        }

        
        [HttpPost]
        public IActionResult AddNotification([FromBody] CreateNotificationDto createDto)
        {
            var notification = _mapper.Map<Notification>(createDto);
            _service.CreateNotificationForRMq(notification);
            var notificationDto = _mapper.Map<ReadNotificationDto>(notification);
            return Ok(notificationDto);
        }

        [HttpGet("{skip}/{take}")]
        public IEnumerable<ReadNotificationDto> GetNotifications(int skip = 0, int take = 50)
        {
            return _service.GetNotifications(skip, take);
        }

        [HttpGet("{id}")]
        public IActionResult GetNotificationByID(int id)
        {
            var notification = _service.GetNotificationByID(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var notification = _service.GetNotificationByID(id);
            if (notification == null) return NotFound();
            _service.DeleteNotificationForRMq(id);
           
            return NoContent();
        }
    }
}
