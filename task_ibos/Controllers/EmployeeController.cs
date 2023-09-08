using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using task_ibos.DTOs;
using task_ibos.Models;

namespace task_ibos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IbossContext _context;

        public EmployeeController(IbossContext dbContext)
        {
            _context = dbContext;
        }

        //API01# Update an employee’s Employee Name and Code [Don't allow duplicate employee code]
        [HttpPost("{id}/update-code")]
        public IActionResult UpdateEmployeeCode(int id, [FromBody] string employeeCode)
        {
            try
            {
                var employee = _context.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound();
                }

                // Check if the new employee code already exists
                var existingEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeCode == employeeCode);
                if (existingEmployee != null && existingEmployee.EmployeeCode == employeeCode)
                {
                    throw new Exception("Employee code already exists.");
                }

                else
                {
                    employee.EmployeeCode = employeeCode;
                    _context.SaveChanges();
                    return Content("Updated successfully");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //API02: Get employee who has 3rd highest salary
        // GET api/employees/third-highest-salary
        [HttpGet("third-highest-salary")]
        public ActionResult<Employee> GetEmployeeWithThirdHighestSalary()
        {
            try
            {
                // Order employees by salary in descending order and skip the first two employees
                var employees = _context.Employees
                    .OrderByDescending(e => e.EmployeeSalary)
                    .Skip(2) // Skip the first two employees to get the third highest
                    .FirstOrDefault();

                if (employees == null)
                {
                    return NotFound("No employee found.");
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //API03# Get all employee based on maximum to minimum salary who has not any absent record
        // GET api/employees/sorted-by-salary
        [HttpGet("sorted-by-salary")]
        public ActionResult<IEnumerable<Employee>> GetEmployeesSortedBySalary()
        {
            try
            {
                var employees = _context.Employees.OrderByDescending(e => e.EmployeeSalary).ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //API04# Get monthly attendance report of all employee
        // GET api/employees/attendance-report/{year}/{month}
        [HttpGet("attendance-report/{year}/{month}")]
        public ActionResult<IEnumerable<object>> GetMonthlyAttendanceReport(int year, int month)
        {
            try
            {
                var employees = _context.Employees.ToList();
                var attendances = _context.EmployeeAttendances.ToList();

                var report = employees.Select(e => new
                {
                    e.EmployeeName,
                    MonthName = new DateTime(year, month, 1).ToString("MMMM"),
                    TotalPresent = attendances.Count(a => a.EmployeeId == e.EmployeeId && a.AttendanceDate.Year == year && a.AttendanceDate.Month == month && a.IsPresent),
                    TotalAbsent = attendances.Count(a => a.EmployeeId == e.EmployeeId && a.AttendanceDate.Year == year && a.AttendanceDate.Month == month && a.IsAbsent),
                    TotalOffday = attendances.Count(a => a.EmployeeId == e.EmployeeId && a.AttendanceDate.Year == year && a.AttendanceDate.Month == month && a.IsOffday)
                }).ToList();

                return Ok(report);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // GET api/employees
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            try
            {
                var employees = _context.Employees.ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // API05# Get a hierarchy from an employee based on his supervisor
        [HttpGet("hierarchy/{employeeId}")]
        public ActionResult<IEnumerable<string>> GetEmployeeHierarchy(int employeeId)
        {
            try
            {
                var hierarchy = new List<string>();
                var employee = _context.Employees.Find(employeeId);

                if (employee == null)
                {
                    return NotFound("Employee not found.");
                }

                // Add the current employee's name to the hierarchy
                hierarchy.Add(employee.EmployeeName);

                // Recursive function to traverse the hierarchy
                void AddSupervisors(Employee currentEmployee)
                {
                    if (currentEmployee.supervisorID != null) ;
                }

                // Start adding supervisors
                AddSupervisors(employee);

                return Ok(hierarchy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }

}