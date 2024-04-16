using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class StudyClass : Entity
    {
        [Required]
        public int YearOfStudy { get; set; }

        [Required]
        public int Credits { get; set; }

        [Required]
        public int Semester { get; set; }

        [Required]
        public ClassType Type { get; set; }

        [Required]
        public string Name { get; set; }

        public StudyClass()
        {
        }
    }
}