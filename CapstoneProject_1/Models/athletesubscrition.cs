using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_1.Models
{
    public class athletesubscritions
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Coach")]
        public int CoachId { get; set; } // FK reference

        public Coach? Coach { get; set; } // Navigation property

        [Required]
        public string Duration { get; set; } = string.Empty; // e.g., "4 weeks"

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Intensity { get; set; } = string.Empty; // e.g., "High"

        [Required]
        public string CreatedBy { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
