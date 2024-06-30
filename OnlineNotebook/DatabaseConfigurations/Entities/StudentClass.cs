using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class StudentClass : Entity
    {
        public User Student { get; private set; }
        public StudyClass Class { get; private set; }
        public int? Grade { get; private set; }

        public StudentClass() { }

        public StudentClass(User student, StudyClass @class, int? grade)
        {
            Student = student;
            Class = @class;
            Grade = grade;
        }

        public void UpdateGrade(int? grade) => Grade = grade;
    }
}
