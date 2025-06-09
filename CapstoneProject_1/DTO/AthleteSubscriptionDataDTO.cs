namespace CapstoneProject_1.DTO
{
    public class AthleteSubscriptionDataDTO
    {
        public int Id { get; set; }

        public int AthleteId { get; set; }

        public int ProgramId { get; set; }

        public int CoachId { get; set; }

        public string CoachName { get; set; } = string.Empty;

        public string Duration { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Intensity { get; set; } = string.Empty;

        public DateTime SubscribedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDateTime { get; set; }
    }
}
