using System.ComponentModel.DataAnnotations;
using System;
using YB.Todo.Convertors;
using System.Text.Json.Serialization;

namespace YB.Todo.DtoModels
{
    public record ToDoItem
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public bool IsComplete { get; set; }

        [Required]
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime CreatedOnUtc { get; set; }

        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? LastModifiedOnUtc { get; set; }
    }
}
