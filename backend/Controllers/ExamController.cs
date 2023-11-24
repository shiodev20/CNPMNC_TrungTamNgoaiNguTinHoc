 using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public ExamController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Exam>>> Get()
        {
            try
            {
                var exams = await _context.Exams.ToListAsync();
                return Ok(exams);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Kỳ thi: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Exam>> Get(int id)
        {
            try
            {
                var exam = await _context.Exams.FindAsync(id);
                if (exam == null)
                    return NotFound("Không tìm thấy Kỳ thi.");

                return Ok(exam);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Kỳ thi: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Exam>>> AddExam(Exam exam)
        {
            try
            {
                _context.Exams.Add(exam);
                await _context.SaveChangesAsync();

                return Ok(await _context.Exams.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Kỳ thi: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Exam>>> UpdateExam(Exam request)
        {
            try
            {
                var dbExam = await _context.Exams.FindAsync(request.examID);
                if (dbExam == null)
                    return NotFound("Không tìm thấy Kỳ thi.");

                dbExam.languageID = request.languageID;
                dbExam.certificateID = request.certificateID;
                dbExam.examName = request.examName;
                dbExam.examDesc = request.examDesc;
                dbExam.examPrice = request.examPrice;

                await _context.SaveChangesAsync();

                return Ok(await _context.Exams.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Kỳ thi: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Exam>>> Delete(int id)
        {
            try
            {
                var dbExam = await _context.Exams.FindAsync(id);
                if (dbExam == null)
                    return NotFound("Không tìm thấy Kỳ thi.");

                _context.Exams.Remove(dbExam);
                await _context.SaveChangesAsync();

                return Ok(await _context.Exams.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Kỳ thi: {ex.Message}");
            }
        }
    }
}
