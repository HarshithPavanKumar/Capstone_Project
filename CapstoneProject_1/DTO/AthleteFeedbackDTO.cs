namespace CapstoneProject_1.DTO
{
    public class AthleteFeedbackDTO
    {
        public int Id { get; set; }

        public int CoachId { get; set; }

        public DateTime Date { get; set; }

        public string Message { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDateTime { get; set; }
    }
}
