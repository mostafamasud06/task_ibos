using System.ComponentModel.DataAnnotations;

namespace task_ibos.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public decimal EmployeeSalary { get; set; }
        [Required]
        public decimal supervisorID { get; set; }


    }
}
