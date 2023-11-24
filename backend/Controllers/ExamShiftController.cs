using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamShiftController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public ExamShiftController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExamShift>>> Get()
        {
            try
            {
                var examShifts = await _context.ExamShifts.ToListAsync();
                return Ok(examShifts);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Ca thi: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamShift>> Get(int id)
        {
            try
            {
                var examShift = await _context.ExamShifts.FindAsync(id);
                if (examShift == null)
                    return NotFound("Không tìm thấy Ca thi.");

                return Ok(examShift);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Ca thi: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<ExamShift>>> AddExamShift(ExamShift examShift)
        {
            try
            {
                _context.ExamShifts.Add(examShift);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamShifts.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Ca thi: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<ExamShift>>> UpdateExamShift(ExamShift request)
        {
            try
            {
                var dbExamShift = await _context.ExamShifts.FindAsync(request.examstID);
                if (dbExamShift == null)
                    return NotFound("Không tìm thấy Ca thi.");

                dbExamShift.branchID = request.branchID;
                dbExamShift.examtID = request.examtID;
                dbExamShift.examsID = request.examsID;
                dbExamShift.examstSize = request.examstSize;
                dbExamShift.examstStartH = request.examstStartH;
                dbExamShift.examstEndH = request.examstEndH;
                dbExamShift.examstDate = request.examstDate;


                await _context.SaveChangesAsync();

                return Ok(await _context.ExamShifts.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Ca thi: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ExamShift>>> Delete(int id)
        {
            try
            {
                var dbExamShift = await _context.ExamShifts.FindAsync(id);
                if (dbExamShift == null)
                    return NotFound("Không tìm thấy Ca thi.");

                _context.ExamShifts.Remove(dbExamShift);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamShifts.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Ca thi: {ex.Message}");
            }
        }
    }
}
