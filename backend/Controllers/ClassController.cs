using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public ClassController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Class>>> Get()
        {
            try
            {
                var classes = await _context.Classes.ToListAsync();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Lớp học: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> Get(int id)
        {
            try
            {
                var classEntity = await _context.Classes.FindAsync(id);
                if (classEntity == null)
                    return NotFound("Không tìm thấy Lớp học.");

                return Ok(classEntity);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Lớp học: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Class>>> AddClass(Class classEntity)
        {
            try
            {
                _context.Classes.Add(classEntity);
                await _context.SaveChangesAsync();

                return Ok(await _context.Classes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Lớp học: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Class>>> UpdateClass(Class request)
        {
            try
            {
                var dbClass = await _context.Classes.FindAsync(request.classID);
                if (dbClass == null)
                    return NotFound("Không tìm thấy Lớp học.");

                dbClass.branchID = request.branchID;
                dbClass.courseID = request.courseID;
                dbClass.className = request.className;
                dbClass.classSize = request.classSize;
                dbClass.classStartD = request.classStartD;
                dbClass.classEndD = request.classEndD;
                dbClass.classStatus = request.classStatus;

                await _context.SaveChangesAsync();

                return Ok(await _context.Classes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Lớp học: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Class>>> Delete(int id)
        {
            try
            {
                var dbClass = await _context.Classes.FindAsync(id);
                if (dbClass == null)
                    return NotFound("Không tìm thấy Lớp học.");

                _context.Classes.Remove(dbClass);
                await _context.SaveChangesAsync();

                return Ok(await _context.Classes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Lớp học: {ex.Message}");
            }
        }
    }
}
