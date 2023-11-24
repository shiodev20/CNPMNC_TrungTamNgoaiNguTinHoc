using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public CourseController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Course>>> Get()
        {
            try
            {
                var courses = await _context.Courses.ToListAsync();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Khóa học: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> Get(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                    return NotFound("Không tìm thấy Khóa học.");

                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Khóa học: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Course>>> AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

                return Ok(await _context.Courses.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Khóa học: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Course>>> UpdateCourse(Course request)
        {
            try
            {
                var dbCourse = await _context.Courses.FindAsync(request.courseID);
                if (dbCourse == null)
                    return NotFound("Không tìm thấy Khóa học.");

                dbCourse.languageID = request.languageID;
                dbCourse.certificateID = request.languageID;
                dbCourse.courseName = request.courseName;
                dbCourse.courseDesc = request.courseDesc;
                dbCourse.coursePrice = request.coursePrice;

                await _context.SaveChangesAsync();

                return Ok(await _context.Courses.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Khóa học: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Course>>> Delete(int id)
        {
            try
            {
                var dbCourse = await _context.Courses.FindAsync(id);
                if (dbCourse == null)
                    return NotFound("Không tìm thấy Khóa học.");

                _context.Courses.Remove(dbCourse);
                await _context.SaveChangesAsync();

                return Ok(await _context.Courses.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Khóa học: {ex.Message}");
            }
        }
    }
}
