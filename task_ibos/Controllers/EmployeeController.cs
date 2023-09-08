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

                else {
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


        



        // GET api/employees/absent
        [HttpGet("absent")]
        public ActionResult<IEnumerable<Employee>> GetAbsentEmployees()
        {
            try
            {
                var employeeIds = _context.EmployeeAttendances
                    .Where(a => a.IsAbsent)
                    .Select(a => a.EmployeeId)
                    .Distinct()
                    .ToList();

                var employees = _context.Employees
                    .Where(e => employeeIds.Contains(e.EmployeeId))
                    .ToList();

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




    
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


    }

}
