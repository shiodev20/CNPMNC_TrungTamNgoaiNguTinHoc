using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamSessionController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public ExamSessionController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExamSession>>> Get()
        {
            try
            {
                var examSessions = await _context.ExamSessions.ToListAsync();
                return Ok(examSessions);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Phiên thi: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamSession>> Get(int id)
        {
            try
            {
                var examSession = await _context.ExamSessions.FindAsync(id);
                if (examSession == null)
                    return NotFound("Không tìm thấy Phiên thi.");

                return Ok(examSession);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Phiên thi: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<ExamSession>>> AddExamSession(ExamSession examSession)
        {
            try
            {
                _context.ExamSessions.Add(examSession);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamSessions.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Phiên thi: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<ExamSession>>> UpdateExamSession(ExamSession request)
        {
            try
            {
                var dbExamSession = await _context.ExamSessions.FindAsync(request.examsID);
                if (dbExamSession == null)
                    return NotFound("Không tìm thấy Phiên thi.");

                dbExamSession.examID = request.examID;
                dbExamSession.examsName = request.examsName;
                dbExamSession.examsStatus = request.examsStatus;

                await _context.SaveChangesAsync();

                return Ok(await _context.ExamSessions.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Phiên thi: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ExamSession>>> Delete(int id)
        {
            try
            {
                var dbExamSession = await _context.ExamSessions.FindAsync(id);
                if (dbExamSession == null)
                    return NotFound("Không tìm thấy Phiên thi.");

                _context.ExamSessions.Remove(dbExamSession);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamSessions.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Phiên thi: {ex.Message}");
            }
        }
    }
}
