using BookProject.Data;
using BookProject.Helper;
using BookProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoversController : ControllerBase
    {
        public readonly ApplicationDbContext _context;

        public CoversController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] BookCover cover)
        {
            cover.ImageUrl = await FileHelper.UploadImage(cover.ImageFile);
            await _context.BookCovers.AddAsync(cover);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetCovers(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;
            var covers = await (from cover in _context.BookCovers
                                 select new
                                 {
                                     Id = cover.Id,
                                     Title = cover.Title,
                                     ImageUrl = cover.ImageUrl,
                                     WriterId = cover.BookWriterId
                                 }).ToListAsync();
            return Ok(covers.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> CoverDetails(int id)
        {
            var cover = await (_context.BookCovers.Include(x => x.Books).
                Where(x => x.Id == id).FirstOrDefaultAsync());
            return Ok(cover);
        }
    }
}
