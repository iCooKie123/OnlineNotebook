using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class StudyClass : Entity
    {
        public int YearOfStudy { get; set; }
        public int Credits { get; set; }
        public int Grade { get; set; }
        public int Semester { get; set; }
        public ClassType Type { get; set; }
        public IEnumerable<User> Students { get; private set; }
    }
}