using System.ComponentModel.DataAnnotations;

namespace LabIV_ToDoList.Models
{
    public class UserDto
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }
        [Required]
        public string address { get; set; }
    }
}
