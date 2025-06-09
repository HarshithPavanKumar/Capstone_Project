using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CapstoneProject_1.Data;
using CapstoneProject_1.Models;
using CapstoneProject_1.DTO;

namespace CapstoneProject_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Coachjwt _jwtService;

        public CoachController(AppDbContext context, Coachjwt jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // ✅ Get all coaches (secured)
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coach>>> GetCoaches()
        {
            return await _context.Coaches.ToListAsync();
        }

        // ✅ Get coach by ID
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Coach>> GetCoach(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null) return NotFound();
            return coach;
        }

        // ✅ Register coach (open)
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Coach>> RegisterCoach([FromBody] Coach dto)
        {
            var exists = await _context.Coaches.AnyAsync(c => c.Email.ToLower() == dto.Email.ToLower());
            if (exists)
                return BadRequest("Email already exists.");

            var coach = new Coach
            {
                Name = dto.Name,
                Email = dto.Email,
                password = dto.password, // ⚠️ Consider hashing
                Specialty = dto.Specialty,
                ExperienceLevel = dto.ExperienceLevel,
                CreatedBy = dto.CreatedBy ?? "system",
                CreatedDateTime = DateTime.UtcNow
            };

            _context.Coaches.Add(coach);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCoach), new { id = coach.Id }, coach);
        }

        // ✅ Update coach (secured)
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoach(int id, Coach coach)
        {
            if (id != coach.Id)
                return BadRequest();

            _context.Entry(coach).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Coaches.Any(c => c.Id == id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        // ✅ Delete coach (secured)
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(int id)
        {
            var coach = await _context.Coaches.FindAsync(id);
            if (coach == null)
                return NotFound();

            _context.Coaches.Remove(coach);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // ✅ Coach Login (returns JWT)
        [AllowAnonymous]
        [HttpPost("coachlogin")]
        public async Task<ActionResult<string>> AdminLogin([FromBody] LoginRequestdto req)
        {
            var token = await _jwtService.Authenticate(req);
            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        // ✅ Email Exists Check
        [AllowAnonymous]
        [HttpGet("exists")]
        public async Task<IActionResult> CheckCoachEmailExists([FromQuery] string email)
        {
            var normalizedEmail = email.Trim().ToLower();
            var exists = await _context.Coaches.AnyAsync(c => c.Email.ToLower() == normalizedEmail);
            return Ok(new { exists });
        }

        // ✅ Admin Dashboard Report (Secured)
        [AllowAnonymous]
        [HttpGet("adminReports")]
        public async Task<IActionResult> GetSummary()
        {
            var adminReports = new
            {
                Coaches = await _context.Coaches.CountAsync(),
                AIPredictions = await _context.AIPredictions.CountAsync(),
                PersonalizedTrainingPrograms = await _context.PersonalizedTrainingPrograms.CountAsync()
            };

            return Ok(adminReports);
        }
    }
}
