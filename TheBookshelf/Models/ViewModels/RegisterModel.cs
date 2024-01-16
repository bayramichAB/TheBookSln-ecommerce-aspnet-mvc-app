using System.ComponentModel.DataAnnotations;

namespace TheBookshelf.Models.ViewModels
{
    public class RegisterModel
    {
        public string? Id { get; set; }
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string? FullName { get; set; } = string.Empty;

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string? Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; } = string.Empty;

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? ConfirmPassword { get; set; }

    }
}
