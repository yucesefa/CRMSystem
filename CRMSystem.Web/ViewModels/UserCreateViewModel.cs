using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Web.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
