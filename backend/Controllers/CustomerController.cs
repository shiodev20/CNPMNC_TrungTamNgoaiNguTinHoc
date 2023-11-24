using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public CustomerController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> Get()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Khách hàng: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(int id)
        {
            try
            {
                var customer = await _context.Customers.FindAsync(id);
                if (customer == null)
                    return NotFound("Không tìm thấy Khách hàng.");

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Khách hàng: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> AddCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return Ok(await _context.Customers.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Khách hàng: {ex.Message}");
            }
        }
    }
}
