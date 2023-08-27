
using System.ComponentModel.DataAnnotations;

namespace Bank_MVC.Models
{
    public class Login
    {
        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
