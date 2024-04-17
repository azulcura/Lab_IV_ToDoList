using System.ComponentModel.DataAnnotations;

namespace LabIV_ToDoList.Models
{
    public class TodoItemDto
    {
        
        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}

