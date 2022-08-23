using System.ComponentModel.DataAnnotations;

namespace YB.Todo.DtoModels
{
    public record AddToDoItem
    {
        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
