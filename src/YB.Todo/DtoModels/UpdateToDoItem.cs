using System.ComponentModel.DataAnnotations;

namespace YB.Todo.DtoModels
{
    public class UpdateToDoItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
