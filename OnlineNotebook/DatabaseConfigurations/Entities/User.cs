using System.ComponentModel.DataAnnotations;
using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

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

        [Required]
        public UserRoles Role { get; private set; }

        // Admin
        public User(string email, string password, string firstName, string lastName)
        {
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = UserRoles.Admin;
        }

        //Student
        public User(
            string email,
            string password,
            string firstName,
            string lastName,
            int? yearOfStudy,
            string? learningCycle,
            string? faculty,
            string? specialization,
            string? group,
            UserRoles role = UserRoles.Student
        )
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
            Role = role;
        }


        public void UpdatePassword(string password) => Password = password;
    }
}
