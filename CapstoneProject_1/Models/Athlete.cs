using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_1.Models
{
    public class Athlete
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty; // Default value
        [Required]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [Range(10, 100)]
        public int Age { get; set; }
        [Required]
        [StringLength(10)]
        public string? Gender { get; set; } // Nullable
        [Required]
        public double Height { get; set; } // in cm
        [Required]
        public double Weight { get; set; } // in kg
        [Required]
        [StringLength(10)]
        public string? Contact { get; set; } // Nullable
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}
