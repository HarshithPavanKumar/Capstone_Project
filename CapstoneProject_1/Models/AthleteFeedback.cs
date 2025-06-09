using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject_1.Models
{
    public class AthleteFeedback
    {
        [Key]
        public int Id { get; set; }

        // Foreign key for Coach
        [ForeignKey("Coach")]
        [Required]
        public int CoachId { get; set; }
        public Coach Coach { get; set; }  // Navigation property

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; } = string.Empty;

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
