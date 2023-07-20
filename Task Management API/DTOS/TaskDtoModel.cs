using System.ComponentModel.DataAnnotations;

namespace Task_Management_API.DTOS
{
    public class TaskDtoModel
    {
        [MaxLength(150)]
        public string? Title { get; set; }

        [MaxLength(1500)]
        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        public bool? IsCompleted { get; set; }
    }
}
