namespace CapstoneProject_1.DTO
{
    public class PerformanceMetricsDTO
    {
        public int Id { get; set; }
        public int AthleteId { get; set; } // Only the foreign key, no navigation property

        public double AverageSpeed { get; set; }
        public double BestSpeed { get; set; }
        public double DistanceCovered { get; set; }
        public int SkillLevel { get; set; }
        public double Improvement { get; set; }
        public double Accuracy { get; set; }
        public double CaloriesBurned { get; set; }
        public double Duration { get; set; }

        public DateTime Timestamp { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
