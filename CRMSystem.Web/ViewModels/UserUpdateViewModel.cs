using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Web.ViewModels
{
    public class UserUpdateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
