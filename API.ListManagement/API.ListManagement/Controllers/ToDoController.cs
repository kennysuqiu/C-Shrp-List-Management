using API.ListManagement.Database;
using ListManagement.models;
using Microsoft.AspNetCore.Mvc;

namespace API.ListManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToDoController : ControllerBase
    {
        private List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        private readonly ILogger<ToDoController> _logger;

        public ToDoController(ILogger<ToDoController> logger)
        {
            _logger = logger;
        }

        [HttpGet()]
        public IEnumerable<Item> Get()
        {
            return FakeDatabase.Items;
        }

        [HttpGet("GetAnInt/{index}")]
        public ActionResult GetAnInt(int index)
        {
            if (index > FakeDatabase.ints.Count)
            {
                return BadRequest();
            } 
            return Ok(FakeDatabase.ints[index]);
        }

        [HttpGet("AddNext")]
        public int AddNext()
        {
            var max = FakeDatabase.ints.Max() + 1;
            FakeDatabase.ints.Add(max);
            return max;
        }
    }
}