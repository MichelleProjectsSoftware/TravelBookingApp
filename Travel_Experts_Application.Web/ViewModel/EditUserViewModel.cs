using System.ComponentModel.DataAnnotations;

namespace Travel_Experts_Application.Web.ViewModel
{
    public class EditUserViewModel
    {
        public EditUserViewModel()
        {
            Roles = new List<string>();
        }
        public string Id { get; set; }


        [Required(ErrorMessage = "User Name is required")]
        [MaxLength(100)]
        public string? UserName { get; set; }

        public IList<string> Roles { get; set; }
    }
}
