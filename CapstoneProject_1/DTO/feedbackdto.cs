using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_1.DTO
{
    [Keyless]
    public class feedbackdto
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; } // Assuming it's a time-based duration
        public string message { get; set; }
         // Assuming Intensity is an integer, adjust if it's different
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
