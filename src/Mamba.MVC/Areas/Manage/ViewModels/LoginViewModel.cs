using System.ComponentModel.DataAnnotations;

namespace Mamba.MVC.Areas.Manage.ViewModels
{
    public class LoginViewModel
    {

        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 2)]
        public string Username { get; set; }
        [Required]
        [StringLength(maximumLength: 25, MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
