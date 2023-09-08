namespace task_ibos.DTOs
{
    public class EmployeeAttendanceDTO
    {
          
            public string EmployeeName { get; set; }
            public string EmployeeCode { get; set; }
            public decimal EmployeeSalary { get; set; }
            public string Date { get; set; }
            public bool IsPresent { get; set; }
            public bool IsAbsent { get; set; }
            public bool IsOffday { get; set; }
        
    }
}
