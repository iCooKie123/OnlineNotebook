using System.ComponentModel.DataAnnotations;

namespace OnlineNotebook.DatabaseConfigurations.Entities.Abstractions
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}