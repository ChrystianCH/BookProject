using Book.Service.Dtos;
using Book.Service.Entities;
using Book.Service.Extenstions;
using Book.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Book.Service.Controllers
{

    // https://localhost:5001/Books
    [ApiController]
    [Route("books")]
    public class BooksController: ControllerBase {

        private readonly BookRepository bookRepository = new();
        private static readonly List<BooksDto> books = new() {

            new BooksDto(1, "Wiedźmin: Ostatnie życzenie", "Andrzej Sapkowski", 59.99, DateTimeOffset.UtcNow),
            new BooksDto(2, "Wiedźmin: Miecze przeznaczenia", "Andrzej Sapkowski", 50.99, DateTimeOffset.UtcNow),
            new BooksDto(3, "Wiedźmin: Krew Elfów", "Andrzej Sapkowski", 69.99, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public async Task<IEnumerable<BooksDto>> GetAsync() {

            var books = (await bookRepository.GetAllAsync()).Select(book => book.AsDto());
            return books;
        }

        // GET /items/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<BooksDto>> GetByIdAsync(int id) {

            SingleBook book = await bookRepository.GetAsync(id);

            if (book == null) {

                return NotFound();
            }

            return book.AsDto();
        }

        // Post /items
        [HttpPost]
        public async Task<ActionResult<BooksDto>> PostAsync(CreateBookDto createBookDto) {

            SingleBook book = new SingleBook{
                BookName = createBookDto.BookName,
                Author = createBookDto.Author,
                Price = createBookDto.Price,
                ReleaseDate = DateTimeOffset.UtcNow
            };

            await bookRepository.CreateAsync(book);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = book.Id}, book);
        }

        // PUT /items/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<BooksDto>> PutAsync(int id, UpdateBookDto updateBookDto) {

            SingleBook existingBook = await bookRepository.GetAsync(id);

            if (existingBook == null) {
                return NotFound();
            }

            existingBook.BookName = updateBookDto.BookName;
            existingBook.Author = updateBookDto.Author;
            existingBook.Price = updateBookDto.Price;

            await bookRepository.UpdateAsync(existingBook);

            return Accepted();
        }

        // Delete /items/{id}
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(int id) {

            SingleBook book = await bookRepository.GetAsync(id);

            if (book == null) {

                return NotFound();
            }

            await bookRepository.RemoveAsync(book.Id);

            return Accepted();
        }
    }
}
