using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_1.Models
{
    public class AthleteSubscriptionData
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("Athlete")]
        public int AthleteId { get; set; }
        public Athlete? Athlete { get; set; }

        [Required, ForeignKey("TrainingProgram")]
        public int ProgramId { get; set; }
        public AthleteTrainingProgram? TrainingProgram { get; set; }

        [Required]
        public DateTime SubscribedAt { get; set; }

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
