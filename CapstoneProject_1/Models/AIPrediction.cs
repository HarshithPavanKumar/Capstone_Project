using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_1.Models
{
    public class AIPrediction
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Athlete")]
        public int AthleteId { get; set; }
        [Required]
        public Athlete Athlete { get; set; }
        [Required]
        public int PredictedSkillLevel { get; set; }
        [Required]
        [StringLength(250)]
        public string Recommendation { get; set; } // e.g., "Increase intensity", "Focus on stamina"
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
