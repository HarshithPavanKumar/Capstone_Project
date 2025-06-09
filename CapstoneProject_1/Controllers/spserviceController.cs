using CapstoneProject_1.Data;
using CapstoneProject_1.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_1.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class spserviceController : Controller
    {
        private readonly AppDbContext app;
        public spserviceController(AppDbContext app)
        {
            this.app = app;
        }
        [HttpGet("trainingprogram")]
        public async Task<ActionResult<IEnumerable<Trainingprogramdto>>> gettraininginfo()
        {
            var results = await app.TrainingProgramsdto
            .FromSqlRaw("EXEC coachallname")
            .ToListAsync();
            Console.WriteLine(results);
            return Ok(results);
        }
        [HttpGet("feedbackinfo")]
        public async Task<ActionResult<IEnumerable<feedbackdto>>> getfeedback()
        {
            var results = await app.feedbackdto
            .FromSqlRaw("EXEC  feedbackinfo")
            .ToListAsync();
            Console.WriteLine(results);
            return Ok(results);
        }
    }
}
