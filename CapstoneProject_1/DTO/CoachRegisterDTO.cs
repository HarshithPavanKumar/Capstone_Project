using System.ComponentModel.DataAnnotations;

namespace CapstoneProject_1.DTO
{
    public class CoachRegisterDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name must contain only letters and spaces.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        [Required]
        public string Specialty { get; set; }

        [Required(ErrorMessage = "Experience level is required.")]
        [StringLength(50, ErrorMessage = "Experience level cannot exceed 50 characters.")]
        public string ExperienceLevel { get; set; }

        public string CreatedBy { get; set; } = "system";  // Optional, can be overwritten by frontend
    }
}
