using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_1.Models
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        [StringLength(100)]
        public string Specialty { get; set; }
        [Required]
        [StringLength(50)]
        public string ExperienceLevel { get; set; }
        [Required]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDateTime { get; set; }
    }
}