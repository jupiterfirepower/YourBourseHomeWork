using System.ComponentModel.DataAnnotations;
using System;

namespace YB.Todo.DtoModels
{
    public class ToDoItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        [Required]
        public DateTime CreatedOnUtc { get; set; }

        public DateTime? LastModifiedOnUtc { get; set; }
    }
}
