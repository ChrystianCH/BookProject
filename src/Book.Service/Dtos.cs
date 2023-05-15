using System.ComponentModel.DataAnnotations;

namespace Book.Service.Dtos { 

    public record BooksDto(int Id, string BookName, string Author, double Price, DateTimeOffset ReleaseDate);

    public record CreateBookDto([Required] string BookName, [Required] string Author, [Range(0, 1000)] double Price);
  
    public record UpdateBookDto([Required] string BookName, [Required] string Author, [Range(0, 1000)] double Price);

}
