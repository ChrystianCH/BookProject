namespace Book.Service.Dtos { 

    public record BooksDto(Guid Id, string BookName, string Author, double Price, DateTimeOffset ReleaseDate);

    public record CreateBookDto(string BookName, string Author, decimal Price);
  
    public record UpdateBookDto(string BookName, string Author, decimal Price);

}