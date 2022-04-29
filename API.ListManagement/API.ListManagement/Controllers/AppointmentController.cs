using API.ListManagement.database;
using API.ListManagement.EC;
using Library.ListManagement.Standard.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly ILogger<ToDoController> _logger;

        public AppointmentController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<AppointmentDTO> Get()
        {
            return new AppointmentEC().Get();
        }

        [HttpPost("AddOrUpdate")]
        public AppointmentDTO AddOrUpdate([FromBody] AppointmentDTO apt)
        {

            return new AppointmentEC().AddOrUpdate(apt);
        }

        [HttpPost("Delete")]
        public AppointmentDTO Delete([FromBody] DeleteItemDTO deleteItemDTO)
        {
            return new AppointmentEC().Delete(deleteItemDTO.IdToDelete);
        }
    }
}
