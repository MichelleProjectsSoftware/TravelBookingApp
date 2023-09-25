using System.ComponentModel.DataAnnotations;

namespace Travel_Experts_Application.Web.ViewModel
{
    public class LoginViewModel
    {
       public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(100)]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
