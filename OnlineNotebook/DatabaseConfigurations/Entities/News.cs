using OnlineNotebook.DatabaseConfigurations.Entities.Abstractions;

namespace OnlineNotebook.DatabaseConfigurations.Entities
{
    public class News(string title, string content) : Entity
    {
        public string Title { get; private set; } = title;
        public string Content { get; private set; } = content;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public void UpdateModifiedAt() => UpdatedAt = DateTime.UtcNow;

        public void UpdateTitle(string title) => Title = title;

        public void UpdateContent(string content) => Content = content;
    }
}
