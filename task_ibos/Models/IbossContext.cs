using Microsoft.EntityFrameworkCore;

namespace task_ibos.Models
{
    public class IbossContext:DbContext
    {
        public IbossContext(DbContextOptions option):base(option)
        {

        }
        public DbSet<Employee>Employees { get; set; }
        public DbSet<EmployeeAttendance>EmployeeAttendances { get; set; }
        
    }
}
