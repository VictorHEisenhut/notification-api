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

        /// <summary>
        /// Add a notification to the database
        /// </summary>
        /// <param name="createDto">Object with the necessary fields for the creation</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">If post was successful</response>
        [HttpPost]
        public IActionResult AddNotification([FromBody] CreateNotificationDto createDto)
        {
            var notification = _mapper.Map<Notification>(createDto);
            _service.CreateNotification(notification);
            _service.SaveChanges();
            return CreatedAtAction(nameof(GetNotificationByID), new { id = notification.ID }, notification);
        }
        

        [HttpGet("{skip}/{take}")]
        public IEnumerable<ReadNotificationDto> GetNotifications([FromQuery] int skip = 0, [FromQuery]int take = 50)
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
            _service.DeleteNotification(id);
            return NoContent();

        }
    }
}
