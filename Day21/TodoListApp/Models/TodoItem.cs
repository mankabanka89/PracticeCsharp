using System;
using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = "";

        public bool IsCompleted { get; set; }

        [DataType(DataType.Date)]
        public DateTime? Deadline { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}