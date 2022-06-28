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
    public class BooksController : ControllerBase
    {
        public readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Book book)
        {
            book.ImageUrl = await FileHelper.UploadImage(book.ImageFile);
            book.BookUrl = await FileHelper.UploadUrl(book.BookFile);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks(int? pageNumber, int? pageSize)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 5;
            var books = await (from book in _context.Books
                                select new
                                {
                                    Id = book.Id,
                                    Title = book.Title,
                                    ImageUrl = book.ImageUrl,
                                    BookUrl = book.BookUrl,
                                    Description = book.Description,
                                }).ToListAsync();
            return Ok(books.Skip((currentPageNumber-1)*currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> BookDetails(int id)
        {
            var book = await (_context.Books.
                Where(x => x.Id == id).FirstOrDefaultAsync());
            return Ok(book);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> NewBooks()
        {
            var books = await (from book in _context.Books
                               orderby book.CreatedDate descending
                               select new
                               {
                                   Id = book.Id,
                                   Title = book.Title,
                                   ImageUrl = book.ImageUrl,
                                   BookUrl = book.BookUrl,
                                   Description = book.Description,
                               }).Take(5).ToListAsync();
            return Ok(books);
        }

        //api/searchbook?query=""

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchBook(string query)
        {
            var books = await (from book in _context.Books
                               orderby book.Title.StartsWith(query)
                               select new
                               {
                                   Id = book.Id,
                                   Title = book.Title,
                                   ImageUrl = book.ImageUrl,
                                   BookUrl = book.BookUrl,
                                   Description = book.Description,
                               }).Take(5).ToListAsync();
            return Ok(books);
        }
    }
}
