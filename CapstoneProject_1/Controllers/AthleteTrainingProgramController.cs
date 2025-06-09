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
    public class AthleteTrainingProgramController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AthleteTrainingProgramController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AthleteTrainingProgram
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteTrainingProgramDTO>>> GetTrainingPrograms()
        {
            var programs = await _context.TrainingPrograms.ToListAsync();

            var dtoList = programs.Select(tp => new AthleteTrainingProgramDTO
            {
                Id = tp.Id,
                CoachId = tp.CoachId,
                Duration = tp.Duration,
                Description = tp.Description,
                Intensity = tp.Intensity,
                CreatedBy = tp.CreatedBy,
                CreatedDateTime = tp.CreatedDateTime
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/AthleteTrainingProgram/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteTrainingProgramDTO>> GetTrainingProgram(int id)
        {
            var tp = await _context.TrainingPrograms.FindAsync(id);

            if (tp == null)
                return NotFound();

            var dto = new AthleteTrainingProgramDTO
            {
                Id = tp.Id,
                CoachId = tp.CoachId,
                Duration = tp.Duration,
                Description = tp.Description,
                Intensity = tp.Intensity,
                CreatedBy = tp.CreatedBy,
                CreatedDateTime = tp.CreatedDateTime
            };

            return Ok(dto);
        }

        // PUT: api/AthleteTrainingProgram/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingProgram(int id, AthleteTrainingProgramDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("Id mismatch");

            // Validate CoachId exists
            bool coachExists = await _context.Coaches.AnyAsync(c => c.Id == dto.CoachId);
            if (!coachExists)
                return BadRequest($"Coach with Id {dto.CoachId} does not exist.");

            var existingTP = await _context.TrainingPrograms.FindAsync(id);
            if (existingTP == null)
                return NotFound();

            // Map DTO to entity
            existingTP.CoachId = dto.CoachId;
            existingTP.Duration = dto.Duration;
            existingTP.Description = dto.Description;
            existingTP.Intensity = dto.Intensity;
            existingTP.CreatedBy = dto.CreatedBy;
            existingTP.CreatedDateTime = dto.CreatedDateTime;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingProgramExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/AthleteTrainingProgram
        [HttpPost]
        public async Task<ActionResult<AthleteTrainingProgramDTO>> PostTrainingProgram(AthleteTrainingProgramDTO dto)
        {
            // Validate CoachId exists
            bool coachExists = await _context.Coaches.AnyAsync(c => c.Id == dto.CoachId);
            if (!coachExists)
                return BadRequest($"Coach with Id {dto.CoachId} does not exist.");

            var newProgram = new AthleteTrainingProgram
            {
                CoachId = dto.CoachId,
                Duration = dto.Duration,
                Description = dto.Description,
                Intensity = dto.Intensity,
                CreatedBy = dto.CreatedBy,
                CreatedDateTime = dto.CreatedDateTime
            };

            _context.TrainingPrograms.Add(newProgram);
            await _context.SaveChangesAsync();

            dto.Id = newProgram.Id;

            return CreatedAtAction(nameof(GetTrainingProgram), new { id = newProgram.Id }, dto);
        }

        // DELETE: api/AthleteTrainingProgram/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingProgram(int id)
        {
            var trainingProgram = await _context.TrainingPrograms.FindAsync(id);
            if (trainingProgram == null)
                return NotFound();

            _context.TrainingPrograms.Remove(trainingProgram);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrainingProgramExists(int id)
        {
            return _context.TrainingPrograms.Any(e => e.Id == id);
        }
    }
}
