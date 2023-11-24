using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public LanguageController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Language>>> Get()
        {
            try
            {
                var languages = await _context.Languages.ToListAsync();
                return Ok(languages);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Ngôn ngữ: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Language>> Get(int id)
        {
            try
            {
                var language = await _context.Languages.FindAsync(id);
                if (language == null)
                    return NotFound("Không tìm thấy Ngôn ngữ.");

                return Ok(language);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Ngôn ngữ: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Language>>> AddLanguage(Language language)
        {
            try
            {
                _context.Languages.Add(language);
                await _context.SaveChangesAsync();

                return Ok(await _context.Languages.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Ngôn ngữ: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Language>>> UpdateLanguage(Language request)
        {
            try
            {
                var dbLanguage = await _context.Languages.FindAsync(request.languageID);
                if (dbLanguage == null)
                    return NotFound("Không tìm thấy Ngôn ngữ.");

                dbLanguage.languageName = request.languageName;

                await _context.SaveChangesAsync();

                return Ok(await _context.Languages.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Ngôn ngữ: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Language>>> Delete(int id)
        {
            try
            {
                var dbLanguage = await _context.Languages.FindAsync(id);
                if (dbLanguage == null)
                    return NotFound("Không tìm thấy Ngôn ngữ.");

                _context.Languages.Remove(dbLanguage);
                await _context.SaveChangesAsync();

                return Ok(await _context.Languages.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Ngôn ngữ: {ex.Message}");
            }
        }

    }
}
