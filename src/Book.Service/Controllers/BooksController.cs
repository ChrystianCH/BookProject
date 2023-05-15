using Book.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Book.Service.Controllers {

    // https://localhost:5001/Books
    [ApiController]
    [Route("Books")]
    public class BooksController: ControllerBase {

        private static readonly List<BooksDto> books = new() {
            new BooksDto(Guid.NewGuid(), "Wiedźmin: Ostatnie życzenie", "Andrzej Sapkowski", 59.99, DateTimeOffset.UtcNow),
            new BooksDto(Guid.NewGuid(), "Wiedźmin: Miecze przeznaczenia", "Andrzej Sapkowski", 50.99, DateTimeOffset.UtcNow),
            new BooksDto(Guid.NewGuid(), "Wiedźmin: Krew Elfów", "Andrzej Sapkowski", 69.99, DateTimeOffset.UtcNow),
        };

        public IEnumerable<BooksDto> Get() {
            
            return books;
        }
        // Get /items/5
        [HttpGet("{id}")]
        public BooksDto? GetById(Guid id) {
            var book = books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }
    } 
}
