using FLCManagementProject.Data;
using FLCManagementProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamBatchController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public ExamBatchController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ExamBatch>>> Get()
        {
            try
            {
                var examBatches = await _context.ExamBatches.ToListAsync();
                return Ok(examBatches);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Kỳ thi - Batch: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ExamBatch>> Get(int id)
        {
            try
            {
                var examBatch = await _context.ExamBatches.FindAsync(id);
                if (examBatch == null)
                    return NotFound("Không tìm thấy Kỳ thi - Batch.");

                return Ok(examBatch);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Kỳ thi - Batch: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<ExamBatch>>> AddExamBatch(ExamBatch examBatch)
        {
            try
            {
                _context.ExamBatches.Add(examBatch);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamBatches.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Kỳ thi - Batch: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<ExamBatch>>> UpdateExamBatch(ExamBatch request)
        {
            try
            {
                var dbExamBatch = await _context.ExamBatches.FindAsync(request.examBatchID);
                if (dbExamBatch == null)
                    return NotFound("Không tìm thấy Kỳ thi - Batch.");

                dbExamBatch.examID = request.examID;
                dbExamBatch.examBatchName = request.examBatchName;
                dbExamBatch.examBatchStatus = request.examBatchStatus;

                await _context.SaveChangesAsync();

                return Ok(await _context.ExamBatches.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Kỳ thi - Batch: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ExamBatch>>> Delete(int id)
        {
            try
            {
                var dbExamBatch = await _context.ExamBatches.FindAsync(id);
                if (dbExamBatch == null)
                    return NotFound("Không tìm thấy Kỳ thi - Batch.");

                _context.ExamBatches.Remove(dbExamBatch);
                await _context.SaveChangesAsync();

                return Ok(await _context.ExamBatches.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Kỳ thi - Batch: {ex.Message}");
            }
        }

    }
}
