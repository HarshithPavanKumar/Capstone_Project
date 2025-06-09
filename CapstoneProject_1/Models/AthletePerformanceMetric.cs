using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_1.Models
{
    public class AthletePerformanceMetric
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Athlete")]
        public int AthleteId { get; set; } // Foreign Key referencing Athletes.Id
        public Athlete Athlete { get; set; }

        [Required]
        public double AverageSpeed { get; set; }
        [Required]
        public double BestSpeed { get; set; } // New property for best speed
        [Required]
        public double DistanceCovered { get; set; } // New property for distance covered
        [Required]
        [Range(1, 10)]
        public int SkillLevel { get; set; } // Scale 1-10 or other scale
        [Required]
        public double Improvement { get; set; } // Percentage or other metric
        [Required]
        public double Accuracy { get; set; } // Percentage
        [Required]
        public double CaloriesBurned { get; set; } // New property for calories burned
        [Required]
        public double Duration { get; set; } // in minutes
        [Required]
        public DateTime Timestamp { get; set; } // Timestamp of the performance metric
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
