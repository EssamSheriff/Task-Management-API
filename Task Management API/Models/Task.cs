using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.Models
{
    public class Task
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string Title { get; set; }
        [MaxLength(1500)]
        public string Description { get; set; }

        public DateTime DueDate { get; set;}

        public bool IsCompleted { get; set; }
    }
}
