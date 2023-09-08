using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace task_ibos.Models
{
    [Keyless]
    public class EmployeeAttendance
    {
        public int EmployeeId { get; set; }

        // Navigation property
        [ForeignKey("EmployeeId")]
        public virtual ICollection<Employee> Employee { get; set; }

        public DateTime AttendanceDate { get; set; }
        public bool IsPresent { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsOffday { get; set; }

    }



}
