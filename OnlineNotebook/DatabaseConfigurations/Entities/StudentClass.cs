using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class StudentClass : Entity
    {
        public User Student { get; set; }
        public StudyClass Class { get; set; }
        public int? Grade { get; set; }
    }
}