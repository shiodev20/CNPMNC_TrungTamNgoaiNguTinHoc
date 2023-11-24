using FLCManagementProject.Data;
using FLCManagementProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillCusClassController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public BillCusClassController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<BillCusClass>>> Get()
        {
            try
            {
                var billCusClasses = await _context.BillCusClasses.ToListAsync();
                return Ok(billCusClasses);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Hóa đơn - Khách hàng - Lớp học: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillCusClass>> Get(int id)
        {
            try
            {
                var billCusClass = await _context.BillCusClasses.FindAsync(id);
                if (billCusClass == null)
                    return NotFound("Không tìm thấy Hóa đơn - Khách hàng - Lớp học.");

                return Ok(billCusClass);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Hóa đơn - Khách hàng - Lớp học: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<BillCusClass>>> AddBillCusClass(BillCusClass billCusClass)
        {
            try
            {
                _context.BillCusClasses.Add(billCusClass);
                await _context.SaveChangesAsync();

                return Ok(await _context.BillCusClasses.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Hóa đơn - Khách hàng - Lớp học: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<BillCusClass>>> UpdateBillCusClass(BillCusClass request)
        {
            try
            {
                var dbBillCusClass = await _context.BillCusClasses.FindAsync(request.billCusClassID);
                if (dbBillCusClass == null)
                    return NotFound("Không tìm thấy Hóa đơn - Khách hàng - Lớp học.");

                dbBillCusClass.classID = request.classID;
                dbBillCusClass.customerID = request.customerID;
                dbBillCusClass.billCusClassDayCreate = request.billCusClassDayCreate;
                dbBillCusClass.billCusClassPrice = request.billCusClassPrice;

                await _context.SaveChangesAsync();

                return Ok(await _context.BillCusClasses.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Hóa đơn - Khách hàng - Lớp học: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<BillCusClass>>> Delete(int id)
        {
            try
            {
                var dbBillCusClass = await _context.BillCusClasses.FindAsync(id);
                if (dbBillCusClass == null)
                    return NotFound("Không tìm thấy Hóa đơn - Khách hàng - Lớp học.");

                _context.BillCusClasses.Remove(dbBillCusClass);
                await _context.SaveChangesAsync();

                return Ok(await _context.BillCusClasses.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Hóa đơn - Khách hàng - Lớp học: {ex.Message}");
            }
        }
    }
}
