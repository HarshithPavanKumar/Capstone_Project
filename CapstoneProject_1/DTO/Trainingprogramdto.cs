using Microsoft.EntityFrameworkCore;

namespace CapstoneProject_1.DTO
{
    [Keyless]
    public class Trainingprogramdto
    {
        public int Id { get; set; }
        public int CoachId { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; } // Assuming it's a time-based duration
        public string Description { get; set; }
        public string Intensity { get; set; } // Assuming Intensity is an integer, adjust if it's different
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
