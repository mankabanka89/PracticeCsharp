using System;
using System.ComponentModel.DataAnnotations;

namespace TodoListApp.Models
{
    public class TaskViewModel
    {
        [Required(ErrorMessage = "Описание задачи обязательно")]
        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов")]
        public string Title { get; set; } = "";

        [DataType(DataType.Date)]
        [Display(Name = "Срок выполнения")]
        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}