using System.ComponentModel.DataAnnotations;

namespace Travel_Experts_Application.Web.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "User Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [MaxLength(100)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
        public string? ConfirmPassword { get; set; }
    }
}
