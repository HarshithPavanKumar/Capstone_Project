using CapstoneProject_1.Data;
using CapstoneProject_1.DTO;
using CapstoneProject_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstoneProject_1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteFeedbackController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AthleteFeedbackController(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // GET: api/feedback
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteFeedbackDTO>>> GetFeedbacks()
        {
            var feedbacks = await _context.Feedbacks.AsNoTracking().ToListAsync();

            var dtoList = feedbacks.Select(f => new AthleteFeedbackDTO
            {
                Id = f.Id,
                CoachId = f.CoachId,
                Date = f.Date,
                Message = f.Message,
                CreatedBy = f.CreatedBy,
                CreatedDateTime = f.CreatedDateTime
            }).ToList();

            return Ok(dtoList);
        }

        // GET: api/feedback/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteFeedbackDTO>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
            {
                return NotFound();
            }

            var dto = new AthleteFeedbackDTO
            {
                Id = feedback.Id,
                CoachId = feedback.CoachId,
                Date = feedback.Date,
                Message = feedback.Message,
                CreatedBy = feedback.CreatedBy,
                CreatedDateTime = feedback.CreatedDateTime
            };

            return Ok(dto);
        }

        // POST: api/feedback
        [HttpPost]
        public async Task<ActionResult<AthleteFeedbackDTO>> PostFeedback([FromBody] AthleteFeedbackDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coachExists = await _context.Coaches.AnyAsync(c => c.Id == dto.CoachId);
            if (!coachExists)
                return BadRequest("CoachId is invalid.");

            var feedback = new AthleteFeedback
            {
                CoachId = dto.CoachId,
                Date = dto.Date == default ? DateTime.UtcNow : dto.Date,
                Message = dto.Message,
                CreatedBy = dto.CreatedBy,
                CreatedDateTime = dto.CreatedDateTime == default ? DateTime.UtcNow : dto.CreatedDateTime
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            dto.Id = feedback.Id;
            dto.Date = feedback.Date;
            dto.CreatedDateTime = feedback.CreatedDateTime;

            return CreatedAtAction(nameof(GetFeedback), new { id = feedback.Id }, dto);
        }

        // PUT: api/feedback/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, [FromBody] AthleteFeedbackDTO dto)
        {
            if (id != dto.Id)
                return BadRequest("ID in URL does not match ID in body.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var coachExists = await _context.Coaches.AnyAsync(c => c.Id == dto.CoachId);
            if (!coachExists)
                return BadRequest("CoachId is invalid.");

            var existingFeedback = await _context.Feedbacks.FindAsync(id);
            if (existingFeedback == null)
                return NotFound();

            existingFeedback.CoachId = dto.CoachId;
            existingFeedback.Date = dto.Date;
            existingFeedback.Message = dto.Message;
            existingFeedback.CreatedBy = dto.CreatedBy;
            existingFeedback.CreatedDateTime = dto.CreatedDateTime;

            _context.Entry(existingFeedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/feedback/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
                return NotFound();

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackExists(int id)
        {
            return _context.Feedbacks.Any(e => e.Id == id);
        }
    }
}
