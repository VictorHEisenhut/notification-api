using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NotificationAPI.Data;
using NotificationAPI.Data.Dtos;

namespace NotificationAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly NotificationContext _context;

        public NotificationController(IMapper mapper, NotificationContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        
    }
}
