using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_1.Models
{
    public class PersonalizedTrainingProgram
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Coach")]
        public int CoachId { get; set; }
        [Required]
        public Coach Coach { get; set; }

        [ForeignKey("Athlete")]
        public int AthleteId { get; set; }
        [Required]
        public Athlete Athlete { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string Duration { get; set; }
        [Required]
        [StringLength(20)]
        public string Intensity { get; set; }
        [ForeignKey("AIPrediction")]
        public int AIPredictionId { get; set; }
        public AIPrediction AIPrediction { get; set; }
        [Required]
        public DateTime AssignedDate { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
