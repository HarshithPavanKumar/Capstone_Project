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
    public class PersonalizedTrainingProgramController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonalizedTrainingProgramController(AppDbContext context)
        {
            _context = context;
        }

        // Mapping entity -> DTO
        private PersonalizedTrainingProgramDTO MapToDTO(PersonalizedTrainingProgram program) => new PersonalizedTrainingProgramDTO
        {
            Id = program.Id,
            CoachId = program.CoachId,
            AthleteId = program.AthleteId,
            Description = program.Description,
            Duration = program.Duration,
            Intensity = program.Intensity,
            AIPredictionId = program.AIPredictionId,
            AssignedDate = program.AssignedDate,
            CreatedBy = program.CreatedBy,
            CreatedDateTime = program.CreatedDateTime
        };

        // Mapping DTO -> entity
        private PersonalizedTrainingProgram MapToEntity(PersonalizedTrainingProgramDTO dto) => new PersonalizedTrainingProgram
        {
            Id = dto.Id,
            CoachId = dto.CoachId,
            AthleteId = dto.AthleteId,
            Description = dto.Description,
            Duration = dto.Duration,
            Intensity = dto.Intensity,
            AIPredictionId = dto.AIPredictionId,
            AssignedDate = dto.AssignedDate,
            CreatedBy = dto.CreatedBy,
            CreatedDateTime = dto.CreatedDateTime
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalizedTrainingProgramDTO>>> GetPrograms()
        {
            var programs = await _context.PersonalizedTrainingPrograms
                .Include(p => p.Coach)
                .Include(p => p.Athlete)
                .Include(p => p.AIPrediction)
                .ToListAsync();

            return programs.Select(MapToDTO).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalizedTrainingProgramDTO>> GetProgram(int id)
        {
            var program = await _context.PersonalizedTrainingPrograms
                .Include(p => p.Coach)
                .Include(p => p.Athlete)
                .Include(p => p.AIPrediction)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (program == null) return NotFound();

            return MapToDTO(program);
        }

        [HttpPost]
        public async Task<ActionResult<PersonalizedTrainingProgramDTO>> PostProgram(PersonalizedTrainingProgramDTO dto)
        {
            var program = MapToEntity(dto);
            program.CreatedDateTime = DateTime.UtcNow;
            if (string.IsNullOrEmpty(program.CreatedBy))
            {
                program.CreatedBy = User?.Identity?.Name ?? "System";
            }

            _context.PersonalizedTrainingPrograms.Add(program);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProgram), new { id = program.Id }, MapToDTO(program));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgram(int id, PersonalizedTrainingProgramDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var program = await _context.PersonalizedTrainingPrograms.FindAsync(id);
            if (program == null) return NotFound();

            // Update allowed fields only
            program.CoachId = dto.CoachId;
            program.AthleteId = dto.AthleteId;
            program.Description = dto.Description;
            program.Duration = dto.Duration;
            program.Intensity = dto.Intensity;
            program.AIPredictionId = dto.AIPredictionId;
            program.AssignedDate = dto.AssignedDate;
            // Do not update CreatedBy or CreatedDateTime on PUT

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgram(int id)
        {
            var program = await _context.PersonalizedTrainingPrograms.FindAsync(id);
            if (program == null) return NotFound();

            _context.PersonalizedTrainingPrograms.Remove(program);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
