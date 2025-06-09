namespace CapstoneProject_1.DTO
{
    public class AthleteTrainingProgramDTO
    {
        public int Id { get; set; }

        public int CoachId { get; set; }  // Only coach ID, no nested coach object

        public string Duration { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Intensity { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDateTime { get; set; }
    }
}
