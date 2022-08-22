using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YB.Todo.Entities
{
    [Table("ToDoItems", Schema = "YBTodo")]
    public class ToDoEntity : BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }
    }
}
