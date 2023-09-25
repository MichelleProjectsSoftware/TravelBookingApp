using System.ComponentModel.DataAnnotations;

namespace Travel_Experts_Application.Web.ViewModel
{
    public class CreateRoleViewModel
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Role Name")]
        public string? RoleName { get; set; }
    }
}
