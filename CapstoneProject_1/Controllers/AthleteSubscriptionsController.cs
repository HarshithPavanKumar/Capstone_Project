using CapstoneProject_1.Data;
using CapstoneProject_1.DTO;
using CapstoneProject_1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class AthleteSubscriptionsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AthleteSubscriptionsController(AppDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

     [HttpGet]
     public async Task<ActionResult<IEnumerable<AthleteSubscriptionDataDTO>>> GetSubscriptions()
     {
         var subscriptions = await _context.Subscriptions
             .Include(s => s.TrainingProgram)
                 .ThenInclude(tp => tp.Coach)
             .AsNoTracking()
             .ToListAsync();

         var dtoList = subscriptions.Select(s => new AthleteSubscriptionDataDTO
         {
             Id = s.Id,
             AthleteId = s.AthleteId,
             ProgramId = s.ProgramId,
             CoachName = s.TrainingProgram?.Coach?.Name ?? "",
             Duration = s.TrainingProgram?.Duration ?? "",
             Description = s.TrainingProgram?.Description ?? "",
             Intensity = s.TrainingProgram?.Intensity ?? "",
             SubscribedAt = s.SubscribedAt,
             CreatedBy = s.CreatedBy,
             CreatedDateTime = s.CreatedDateTime
         }).ToList();

         return Ok(dtoList);
     }

     [HttpGet("{id}")]
     public async Task<ActionResult<AthleteSubscriptionDataDTO>> GetSubscription(int id)
     {
         var subscription = await _context.Subscriptions
             .Include(s => s.TrainingProgram)
                 .ThenInclude(tp => tp.Coach)
             .FirstOrDefaultAsync(s => s.Id == id);

         if (subscription == null) return NotFound();

         var dto = new AthleteSubscriptionDataDTO
         {
             Id = subscription.Id,
             AthleteId = subscription.AthleteId,
             ProgramId = subscription.ProgramId,
             CoachName = subscription.TrainingProgram?.Coach?.Name ?? "",
             Duration = subscription.TrainingProgram?.Duration ?? "",
             Description = subscription.TrainingProgram?.Description ?? "",
             Intensity = subscription.TrainingProgram?.Intensity ?? "",
             SubscribedAt = subscription.SubscribedAt,
             CreatedBy = subscription.CreatedBy,
             CreatedDateTime = subscription.CreatedDateTime
         };

         return Ok(dto);
     }

     [HttpPost]
     public async Task<ActionResult<AthleteSubscriptionDataDTO>> Subscribe([FromBody] AthleteSubscriptionData dto)
     {
         if (!ModelState.IsValid)
             return BadRequest(ModelState);

         var exists = await _context.Subscriptions.AnyAsync(s =>
             s.AthleteId == dto.AthleteId && s.ProgramId == dto.ProgramId);

         if (exists)
             return Conflict("Subscription already exists for this athlete and program.");

         var subscription = new AthleteSubscriptionData
         {
             AthleteId = dto.AthleteId,
             ProgramId = dto.ProgramId,
             SubscribedAt = DateTime.UtcNow,
             CreatedBy = dto.CreatedBy,
             CreatedDateTime = dto.CreatedDateTime
         };

         _context.Subscriptions.Add(subscription);
         await _context.SaveChangesAsync();

         dto.Id = subscription.Id;
         dto.SubscribedAt = subscription.SubscribedAt;

         return CreatedAtAction(nameof(GetSubscription), new { id = subscription.Id }, dto);
     }

     [HttpPut("{id}")]
     public async Task<IActionResult> UpdateSubscription(int id, [FromBody] AthleteSubscriptionDataDTO dto)
     {
         if (id != dto.Id)
             return BadRequest("ID mismatch");

         var existing = await _context.Subscriptions.FindAsync(id);
         if (existing == null)
             return NotFound();

         var duplicateExists = await _context.Subscriptions.AnyAsync(s =>
             s.Id != dto.Id && s.AthleteId == dto.AthleteId && s.ProgramId == dto.ProgramId);

         if (duplicateExists)
             return Conflict("Another subscription already exists for this athlete and program.");

         existing.AthleteId = dto.AthleteId;
         existing.ProgramId = dto.ProgramId;
         existing.SubscribedAt = dto.SubscribedAt;
         existing.CreatedBy = dto.CreatedBy;
         existing.CreatedDateTime = dto.CreatedDateTime;

         await _context.SaveChangesAsync();
         return NoContent();
     }

     [HttpDelete("{id}")]
     public async Task<IActionResult> Unsubscribe(int id)
     {
         var subscription = await _context.Subscriptions.FindAsync(id);
         if (subscription == null)
             return NotFound();

         _context.Subscriptions.Remove(subscription);
         await _context.SaveChangesAsync();

         return NoContent();
     }
    /*
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AthleteSubscriptionDataDTO dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var subscription = new athletesubscritions
        {
            CoachId = dto.CoachId,
            Duration = dto.Duration,
            Description = dto.Description,
            Intensity = dto.Intensity,
            CreatedBy = dto.CreatedBy,
            CreatedDateTime = DateTime.UtcNow
        };

        _context.atheletesubscriptions.Add(subscription);
        await _context.SaveChangesAsync();
        return Ok("successfully inserted");
    }*/
}
