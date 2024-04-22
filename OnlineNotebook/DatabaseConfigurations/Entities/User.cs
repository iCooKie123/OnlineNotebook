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

        public int? YearOfStudy { get; private set; }
        public string? LearningCycle { get; set; }
        public string? Faculty { get; private set; }
        public string? Specialization { get; private set; }
        public string? Group { get; private set; }

        public User(string email, string password, string firstName, string lastName, int? yearOfStudy,
                    string? learningCycle, string? faculty, string? specialization, string? group)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            YearOfStudy = yearOfStudy;
            LearningCycle = learningCycle;
            Faculty = faculty;
            Specialization = specialization;
            Group = group;
        }

        public void UpdateEmail(string email) => Email = email;

        public void UpdatePassword(string password) => Password = password;

        public void UpdateFirstName(string firstName) => FirstName = firstName;

        public void UpdateLastName(string lastName) => LastName = lastName;

        public void UpdateYearOfStudy(int? yearOfStudy) => YearOfStudy = yearOfStudy;
    }
}