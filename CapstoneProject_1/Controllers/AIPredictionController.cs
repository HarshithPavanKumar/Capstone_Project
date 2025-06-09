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
    public class AIPredictionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AIPredictionController(AppDbContext context)
        {
            _context = context;
        }

        // Helper to map entity to DTO
        private AIPredictionDTO MapToDTO(AIPrediction prediction) => new AIPredictionDTO
        {
            Id = prediction.Id,
            AthleteId = prediction.AthleteId,
            PredictedSkillLevel = prediction.PredictedSkillLevel,
            Recommendation = prediction.Recommendation,
            CreatedBy = prediction.CreatedBy,
            CreatedDateTime = prediction.CreatedDateTime
        };

        // Helper to map DTO to entity (for POST and PUT)
        private AIPrediction MapToEntity(AIPredictionDTO dto) => new AIPrediction
        {
            Id = dto.Id,
            AthleteId = dto.AthleteId,
            PredictedSkillLevel = dto.PredictedSkillLevel,
            Recommendation = dto.Recommendation,
            CreatedBy = dto.CreatedBy,
            CreatedDateTime = dto.CreatedDateTime
        };

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AIPredictionDTO>>> GetPredictions()
        {
            var predictions = await _context.AIPredictions.ToListAsync();
            return predictions.Select(p => MapToDTO(p)).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AIPredictionDTO>> GetPrediction(int id)
        {
            var prediction = await _context.AIPredictions.FindAsync(id);
            if (prediction == null) return NotFound();
            return MapToDTO(prediction);
        }

        [HttpPost]
        public async Task<ActionResult<AIPredictionDTO>> PostPrediction(AIPredictionDTO dto)
        {
            var prediction = MapToEntity(dto);
            prediction.CreatedDateTime = DateTime.UtcNow;

            _context.AIPredictions.Add(prediction);
            await _context.SaveChangesAsync();

            var resultDto = MapToDTO(prediction);
            return CreatedAtAction(nameof(GetPrediction), new { id = prediction.Id }, resultDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrediction(int id, AIPredictionDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var existingPrediction = await _context.AIPredictions.FindAsync(id);
            if (existingPrediction == null) return NotFound();

            // Update existing entity with DTO values
            existingPrediction.AthleteId = dto.AthleteId;
            existingPrediction.PredictedSkillLevel = dto.PredictedSkillLevel;
            existingPrediction.Recommendation = dto.Recommendation;
            existingPrediction.CreatedBy = dto.CreatedBy;
            // Usually CreatedDateTime is not updated on PUT, but you can if you want

            _context.Entry(existingPrediction).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrediction(int id)
        {
            var prediction = await _context.AIPredictions.FindAsync(id);
            if (prediction == null) return NotFound();

            _context.AIPredictions.Remove(prediction);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
