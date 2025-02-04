using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Task
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        public int ProjectId { get; set; }

        public int? AssignedToId { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }
    }
}
