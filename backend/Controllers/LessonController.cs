using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public LessonController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Lesson>>> Get()
        {
            try
            {
                var lessons = await _context.Lessons.ToListAsync();
                return Ok(lessons);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Bài học: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lesson>> Get(int id)
        {
            try
            {
                var lesson = await _context.Lessons.FindAsync(id);
                if (lesson == null)
                    return NotFound("Không tìm thấy Bài học.");

                return Ok(lesson);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Bài học: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Lesson>>> AddLesson(Lesson lesson)
        {
            try
            {
                _context.Lessons.Add(lesson);
                await _context.SaveChangesAsync();

                return Ok(await _context.Lessons.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Bài học: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Lesson>>> UpdateLesson(Lesson request)
        {
            try
            {
                var dbLesson = await _context.Lessons.FindAsync(request.lessonID);
                if (dbLesson == null)
                    return NotFound("Không tìm thấy Bài học.");

                dbLesson.classID = request.classID;
                dbLesson.lessonStartH = request.lessonStartD;
                dbLesson.lessonEndD = request.lessonEndD;
                dbLesson.lessonShift = request.lessonShift;
                dbLesson.lessonStartH = request.lessonStartH;
                dbLesson.lessonEndH = request.lessonEndH;

                await _context.SaveChangesAsync();

                return Ok(await _context.Lessons.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Bài học: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Lesson>>> Delete(int id)
        {
            try
            {
                var dbLesson = await _context.Lessons.FindAsync(id);
                if (dbLesson == null)
                    return NotFound("Không tìm thấy Bài học.");

                _context.Lessons.Remove(dbLesson);
                await _context.SaveChangesAsync();

                return Ok(await _context.Lessons.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Bài học: {ex.Message}");
            }
        }

    }
}
