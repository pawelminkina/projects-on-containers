using System.ComponentModel.DataAnnotations;

namespace Auth.API.Auth.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel(string returnUrl)
        {
            ReturnUrl = returnUrl;
        }

        //For [FromForm] Body
        public LoginViewModel()
        {

        }

        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public LoginViewModel NullifyPassword()
        {
            Password = null;
            return this;
        }
    }
}
