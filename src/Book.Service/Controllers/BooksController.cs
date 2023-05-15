
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

        // GET /items/{id}
        [HttpGet("{id}")]
        public ActionResult<BooksDto>? GetById(int id) {

            BooksDto? book = books.Where(book => book.Id == id).SingleOrDefault();

            if (book == null) {

                return NotFound();
            }

            return book;
        }

        // Post /items
        [HttpPost]
        public ActionResult<BooksDto> Post(CreateBookDto createBookDto) {

            BooksDto book = new BooksDto(4, createBookDto.BookName, createBookDto.Author, createBookDto.Price, DateTimeOffset.UtcNow);

            books.Add(book);

            return CreatedAtAction(nameof(GetById), new { id = book.Id}, book);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public ActionResult<BooksDto> Put(int id, UpdateBookDto updateBookDto) {

            BooksDto? existingBook = books.Where(book => book.Id == id).SingleOrDefault();
              if (existingBook == null) {

                return NotFound();
            }

            BooksDto updatedBook = existingBook with {
                BookName = updateBookDto.BookName,
                Author = updateBookDto.Author,
                Price = updateBookDto.Price
            };

            var index = books.FindIndex(existingBook => existingBook.Id == id);
            books[index] = updatedBook;

            return Accepted();
        }

        // Delete /items/{id}
        [HttpDelete("{Id}")]
        public IActionResult Delete(int id) {

            var index = books.FindIndex(existingBook => existingBook.Id == id);
          
            if (index < 0) {

                return NotFound();
            }

            books.RemoveAt(index);

            return Accepted();
        }
    }
}
