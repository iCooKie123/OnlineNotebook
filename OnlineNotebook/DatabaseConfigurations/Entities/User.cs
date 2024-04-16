using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class User : Entity
    {
        [Required]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        [Required]
        public string FirstName { get; private set; }

        [Required]
        public string LastName { get; private set; }

        [Required]
        public int? YearOfStudy { get; private set; }

        public User(string email, string password, string firstName, string lastName, int? yearOfStudy)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            YearOfStudy = yearOfStudy;
        }

        public void UpdateEmail(string email) => Email = email;

        public void UpdatePassword(string password) => Password = password;

        public void UpdateFirstName(string firstName) => FirstName = firstName;

        public void UpdateLastName(string lastName) => LastName = lastName;

        public void UpdateYearOfStudy(int? yearOfStudy) => YearOfStudy = yearOfStudy;
    }
}