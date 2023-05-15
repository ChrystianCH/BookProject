
using Book.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Book.Service.Controllers
{

    // https://localhost:5001/Books
    [ApiController]
    [Route("books")]
    public class BooksController: ControllerBase {

        private static readonly List<BooksDto> books = new() {
            new BooksDto(1, "Wiedźmin: Ostatnie życzenie", "Andrzej Sapkowski", 59.99, DateTimeOffset.UtcNow),
            new BooksDto(2, "Wiedźmin: Miecze przeznaczenia", "Andrzej Sapkowski", 50.99, DateTimeOffset.UtcNow),
            new BooksDto(3, "Wiedźmin: Krew Elfów", "Andrzej Sapkowski", 69.99, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<BooksDto> Get() {
            return books;
        }

        // GET /items/5
        [HttpGet("{id}")]
        public BooksDto? GetById(int id) {
            var book = books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        [HttpPost]
        public ActionResult<BooksDto> Post(CreateBookDto createBookDto) {
            var book = new BooksDto(4, createBookDto.BookName, createBookDto.Author, createBookDto.Price, DateTimeOffset.UtcNow);
            books.Add(book);
            return CreatedAtAction(nameof(GetById), new { id = book.Id}, book);
        }
    }
}
