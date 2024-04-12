using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class User : Entity
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public void UpdateEmail(string email) => Email = email;

        public void UpdatePassword(string password) => Password = password;
    }
}