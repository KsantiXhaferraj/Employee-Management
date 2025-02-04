using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
