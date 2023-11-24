using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public CertificateController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Certificate>>> Get()
        {
            try
            {
                var certificates = await _context.Certificates.ToListAsync();
                return Ok(certificates);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Chứng chỉ: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Certificate>> Get(int id)
        {
            try
            {
                var certificate = await _context.Certificates.FindAsync(id);
                if (certificate == null)
                    return NotFound("Không tìm thấy Chứng chỉ.");

                return Ok(certificate);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Chứng chỉ: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Certificate>>> AddCertificate(Certificate certificate)
        {
            try
            {
                _context.Certificates.Add(certificate);
                await _context.SaveChangesAsync();

                return Ok(await _context.Certificates.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Chứng chỉ: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Certificate>>> UpdateCertificate(Certificate request)
        {
            try
            {
                var dbCertificate = await _context.Certificates.FindAsync(request.certificateID);
                if (dbCertificate == null)
                    return NotFound("Không tìm thấy Chứng chỉ.");

                dbCertificate.certificateName = request.certificateName;

                await _context.SaveChangesAsync();

                return Ok(await _context.Certificates.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Chứng chỉ: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Certificate>>> Delete(int id)
        {
            try
            {
                var dbCertificate = await _context.Certificates.FindAsync(id);
                if (dbCertificate == null)
                    return NotFound("Không tìm thấy Chứng chỉ.");

                _context.Certificates.Remove(dbCertificate);
                await _context.SaveChangesAsync();

                return Ok(await _context.Certificates.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Chứng chỉ: {ex.Message}");
            }
        }
    }
}
