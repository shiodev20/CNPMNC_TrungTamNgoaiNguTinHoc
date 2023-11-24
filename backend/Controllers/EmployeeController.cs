using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public EmployeeController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var employees = _context.Employees.ToList();

                if (employees.Count == 0)
                {
                    return NotFound("Chưa có Nhân Viên nào.");
                }

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var employee = _context.Employees.Find(id);
                if (employee == null)
                {
                    return NotFound($"Không có Nhân Viên với ID là: {id}");
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(Employee model)
        {
            try
            {
                _context.Add(model);
                _context.SaveChanges();

                return Ok("Đã thêm Nhân Viên mới.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult Put(Employee model)
        {
            if (model == null || model.employeeID == 0)
            {
                if (model == null)
                {
                    return BadRequest("Dữ liệu không hợp lệ.");
                }
                else if (model.employeeID == 0)
                {
                    return BadRequest($"Nhân Viên có ID là {model.employeeID} không hợp lệ.");
                }
            }

            try
            {
                var employee = _context.Employees.Find(model.employeeID);
                if (employee == null)
                {
                    return NotFound($"Không tìm thấy Nhân Viên có ID là: {model.employeeID}");
                }

                employee.employeeFName = model.employeeFName;
                employee.employeeLName = model.employeeLName;
                employee.employeeEmail = model.employeeEmail;
                employee.employeePhone = model.employeePhone;
                employee.employeeDob = model.employeeDob;

                _context.SaveChanges();

                return Ok("Đã sửa Nhân Viên.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _context.Employees.Find(id);

                if (employee == null)
                {
                    return BadRequest($"Không có Nhân Viên với ID là: {id}");
                }

                _context.Employees.Remove(employee);
                _context.SaveChanges();

                return Ok("Đã xóa thông tin Nhân Viên.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Filter")]
        public IActionResult Filter(string employeeName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    return BadRequest("Tên nhân viên không được trống.");
                }

                var filteredEmployees = _context.Employees
                    .Where(employee =>
                        employee.employeeFName.Contains(employeeName, StringComparison.OrdinalIgnoreCase) ||
                        employee.employeeLName.Contains(employeeName, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (filteredEmployees.Count == 0)
                {
                    return NotFound($"Không có Nhân Viên nào phù hợp với tên: {employeeName}");
                }

                return Ok(filteredEmployees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
