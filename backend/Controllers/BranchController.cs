using FLCManagement.Models;
using FLCManagementProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FLCManagementProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly FLCMDBContext _context;

        public BranchController(FLCMDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Branch>>> Get()
        {
            try
            {
                var branches = await _context.Branches.ToListAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Chi nhánh: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> Get(int id)
        {
            try
            {
                var branch = await _context.Branches.FindAsync(id);
                if (branch == null)
                    return NotFound("Không tìm thấy Chi nhánh.");

                return Ok(branch);
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi lấy dữ liệu Chi nhánh: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<List<Branch>>> AddBranch(Branch branch)
        {
            try
            {
                _context.Branches.Add(branch);
                await _context.SaveChangesAsync();

                return Ok(await _context.Branches.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi thêm Chi nhánh: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<ActionResult<List<Branch>>> UpdateBranch(Branch request)
        {
            try
            {
                var dbBranch = await _context.Branches.FindAsync(request.branchID);
                if (dbBranch == null)
                    return NotFound("Không tìm thấy Chi nhánh.");

                dbBranch.branchName = request.branchName;
                dbBranch.branchAddress = request.branchAddress;

                await _context.SaveChangesAsync();

                return Ok(await _context.Branches.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi cập nhật Chi nhánh: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Branch>>> Delete(int id)
        {
            try
            {
                var dbBranch = await _context.Branches.FindAsync(id);
                if (dbBranch == null)
                    return NotFound("Không tìm thấy Chi nhánh.");

                _context.Branches.Remove(dbBranch);
                await _context.SaveChangesAsync();

                return Ok(await _context.Branches.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest($"Lỗi khi xóa Chi nhánh: {ex.Message}");
            }
        }
    }
}
