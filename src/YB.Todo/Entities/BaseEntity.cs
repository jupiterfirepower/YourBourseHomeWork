using System;
using System.ComponentModel.DataAnnotations;

namespace YB.Todo.Entities
{
    public abstract class BaseEntity
    {
        [Required]
        public DateTime CreatedOnUtc { get; set; }

        public DateTime? LastModifiedOnUtc { get; set; }
    }
}
