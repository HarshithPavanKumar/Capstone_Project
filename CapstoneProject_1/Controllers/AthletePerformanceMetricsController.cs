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
    public class PerformanceMetricsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PerformanceMetricsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PerformanceMetrics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceMetricsDTO>>> GetPerformanceMetrics()
        {
            var metrics = await _context.PerformanceMetrics
                .Select(m => new PerformanceMetricsDTO
                {
                    Id = m.Id,
                    AthleteId = m.AthleteId,
                    AverageSpeed = m.AverageSpeed,
                    BestSpeed = m.BestSpeed,
                    DistanceCovered = m.DistanceCovered,
                    SkillLevel = m.SkillLevel,
                    Improvement = m.Improvement,
                    Accuracy = m.Accuracy,
                    CaloriesBurned = m.CaloriesBurned,
                    Duration = m.Duration,
                    Timestamp = m.Timestamp,
                    CreatedBy = m.CreatedBy,
                    CreatedDateTime = m.CreatedDateTime
                })
                .ToListAsync();

            return Ok(metrics);
        }
        [HttpGet("athlete/{athleteId}")]
        public async Task<IActionResult> GetMetricsByAthleteId(int athleteId)
        {
            var metrics = await _context.PerformanceMetrics
                .Where(m => m.AthleteId == athleteId)
                .ToListAsync();

            if (metrics == null || !metrics.Any())
                return NotFound("No metrics found for this athlete.");

            return Ok(metrics);
        }

        //GET: api/PerformanceMetrics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceMetricsDTO>> GetPerformanceMetric(int id)
        {
            var m = await _context.PerformanceMetrics.FindAsync(id);
            if (m == null) return NotFound();

            var dto = new PerformanceMetricsDTO
            {
                Id = m.Id,
                AthleteId = m.AthleteId,
                AverageSpeed = m.AverageSpeed,
                BestSpeed = m.BestSpeed,
                DistanceCovered = m.DistanceCovered,
                SkillLevel = m.SkillLevel,
                Improvement = m.Improvement,
                Accuracy = m.Accuracy,
                CaloriesBurned = m.CaloriesBurned,
                Duration = m.Duration,
                Timestamp = m.Timestamp,
                CreatedBy = m.CreatedBy,
                CreatedDateTime = m.CreatedDateTime
            };

            return Ok(dto);
        }

        // POST: api/PerformanceMetrics
        [HttpPost]
        public async Task<ActionResult<PerformanceMetricsDTO>> PostPerformanceMetric(PerformanceMetricsDTO dto)
        {
            var entity = new AthletePerformanceMetric
            {
                AthleteId = dto.AthleteId,
                AverageSpeed = dto.AverageSpeed,
                BestSpeed = dto.BestSpeed,
                DistanceCovered = dto.DistanceCovered,
                SkillLevel = dto.SkillLevel,
                Improvement = dto.Improvement,
                Accuracy = dto.Accuracy,
                CaloriesBurned = dto.CaloriesBurned,
                Duration = dto.Duration,
                Timestamp = dto.Timestamp,
                CreatedBy = dto.CreatedBy,
                CreatedDateTime = DateTime.UtcNow
            };

            _context.PerformanceMetrics.Add(entity);
            await _context.SaveChangesAsync();

            dto.Id = entity.Id;
            dto.CreatedDateTime = entity.CreatedDateTime;

            return CreatedAtAction(nameof(GetPerformanceMetric), new { id = dto.Id }, dto);
        }

        // PUT: api/PerformanceMetrics/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformanceMetric(int id, PerformanceMetricsDTO dto)
        {
            if (id != dto.Id) return BadRequest();

            var entity = await _context.PerformanceMetrics.FindAsync(id);
            if (entity == null) return NotFound();

            entity.AthleteId = dto.AthleteId;
            entity.AverageSpeed = dto.AverageSpeed;
            entity.BestSpeed = dto.BestSpeed;
            entity.DistanceCovered = dto.DistanceCovered;
            entity.SkillLevel = dto.SkillLevel;
            entity.Improvement = dto.Improvement;
            entity.Accuracy = dto.Accuracy;
            entity.CaloriesBurned = dto.CaloriesBurned;
            entity.Duration = dto.Duration;
            entity.Timestamp = dto.Timestamp;
            entity.CreatedBy = dto.CreatedBy;

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/PerformanceMetrics/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerformanceMetric(int id)
        {
            var entity = await _context.PerformanceMetrics.FindAsync(id);
            if (entity == null) return NotFound();

            _context.PerformanceMetrics.Remove(entity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}