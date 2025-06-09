using CapstoneProject_1.Data;
using CapstoneProject_1.DTO;
using CapstoneProject_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AthletesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly userjwt j;
        public AthletesController(AppDbContext context, userjwt j)
        {
            _context = context;
            this.j = j;
        }

        // GET: api/athletes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Athlete>>> GetAllAthletes()
        {
            return await _context.Athletes.ToListAsync();
        }

        // GET: api/athletes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Athlete>> GetAthlete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);

            if (athlete == null)
            {
                return NotFound();
            }

            return athlete;
        }


        // POST: api/athletes
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Athlete>> CreateAthlete([FromBody] Athlete athlete) //read the request body and deserialize it into the specified object.
        {
            if (!ModelState.IsValid) //validation state of model binding
            {
                return BadRequest(ModelState);
            }

            _context.Athletes.Add(athlete);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAthlete), new { id = athlete.Id }, athlete);
        }

        // PUT: api/athletes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAthlete(int id, [FromBody] Athlete athlete)
        {
            if (id != athlete.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Entry(athlete).State = EntityState.Modified; //entry - tracks metadata and state information ; 

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) //if multiple users/processes try to modify the same record at the same time
            {
                if (!AthleteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/athletes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAthlete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);

            if (athlete == null)
            {
                return NotFound();
            }

            _context.Athletes.Remove(athlete);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.Id == id);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> userlogin([FromBody] LoginRequestdto req)
        {
            var res = await j.Authenticate(req);
            if (res == null)
            {
                return Unauthorized();
            }
            return Ok(res);
        }
        [AllowAnonymous]
        [HttpGet("exists")]
        public async Task<IActionResult> EmailExists([FromQuery] String email)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(e => e.Email == email);
            return Ok(new { exists = athlete != null });
        }

        [HttpGet("byemail")]
        public async Task<ActionResult<Athlete>>GetEmailAthlete(string email)
        {
            var athlete = await _context.Athletes.FirstOrDefaultAsync(e=>e.Email==email);

            if (athlete == null)
            {
                return NotFound();
            }

            return athlete;
        }

        [AllowAnonymous]
        [HttpGet("Reports")]
        public async Task<IActionResult> GetSummary()
        {
            var Reports = new
            {
                Athletes = await _context.Athletes.CountAsync(),
                PerformanceMetrics = await _context.PerformanceMetrics.CountAsync(),
                TrainingPrograms = await _context.TrainingPrograms.CountAsync(),
                Subscriptions = await _context.Subscriptions.CountAsync(),
                Feedbacks = await _context.Feedbacks.CountAsync(),
            };
            return Ok(Reports);
        }

    }
}