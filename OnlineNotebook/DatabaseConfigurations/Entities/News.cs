using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class News(string title, string content) : Entity
    {
        public string Title { get; private set; } = title;
        public string Content { get; private set; } = content;
    }
}
