using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamTypeController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public ExamTypeController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExamType>>> Get()
        {
            try
            {
                var examTypes = await _context.ExamTypes.ToListAsync();
                return Ok(examTypes);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Loại kỳ thi: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamType>> Get(int id)
        {
            try
            {
                var examType = await _context.ExamTypes.FindAsync(id);
                if (examType == null)
                    return NotFound("Không tìm thấy Loại kỳ thi.");

                return Ok(examType);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Loại kỳ thi: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<ExamType>>> AddExamType(ExamType examType)
        {
            try
            {
                _context.ExamTypes.Add(examType);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamTypes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Loại kỳ thi: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<ExamType>>> UpdateExamType(ExamType request)
        {
            try
            {
                var dbExamType = await _context.ExamTypes.FindAsync(request.examtID);
                if (dbExamType == null)
                    return NotFound("Không tìm thấy Loại kỳ thi.");

                dbExamType.examtName = request.examtName;

                await _context.SaveChangesAsync();

                return Ok(await _context.ExamTypes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Loại kỳ thi: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ExamType>>> Delete(int id)
        {
            try
            {
                var dbExamType = await _context.ExamTypes.FindAsync(id);
                if (dbExamType == null)
                    return NotFound("Không tìm thấy Loại kỳ thi.");

                _context.ExamTypes.Remove(dbExamType);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamTypes.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Loại kỳ thi: {ex.Message}");
            }
        }

    }
}