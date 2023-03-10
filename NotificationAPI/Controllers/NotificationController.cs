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
        /// Adds a notification to the database
        /// </summary>
        /// <param name="createDto">Object with necessary fields for creation</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">If successful</response>
        [HttpPost]
        public IActionResult AddNotification([FromBody] CreateNotificationDto createDto)
        {
            var notification = _mapper.Map<Notification>(createDto);
            _service.CreateNotificationForRMq(notification);
            var notificationDto = _mapper.Map<ReadNotificationDto>(notification);
            return Ok(notificationDto);
        }

        /// <summary>
        /// Gets all notifications from database
        /// </summary>
        /// <param name="skip">Skips a defined amount of notifications</param>
        /// <param name="take">Takes a defined amount of notifications</param>
        /// <returns>IEnumerable</returns>
        /// <response code="200">If successful</response>
        [HttpGet("{skip}/{take}")]
        public IEnumerable<ReadNotificationDto> GetNotifications(int skip = 0, int take = 50)
        {
            return _service.GetNotifications(skip, take);
        }

        /// <summary>
        /// Gets a notification according to it's ID
        /// </summary>
        /// <param name="id">Unique property that represents each notification</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">If successful</response>
        [HttpGet("{id}")]
        public IActionResult GetNotificationByID(int id)
        {
            var notification = _service.GetNotificationByID(id);
            if (notification == null) return NotFound();
            return Ok(notification);
        }

        /// <summary>
        /// Deletes a notification 
        /// </summary>
        /// <param name="id">Unique property that represents each notification</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">If successful</response>
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
