namespace CapstoneProject_1.DTO
{
    public class AIPredictionDTO
    {
        public int Id { get; set; }

        public int AthleteId { get; set; }

        public int PredictedSkillLevel { get; set; }

        public string Recommendation { get; set; } = string.Empty;

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedDateTime { get; set; }
    }
}
