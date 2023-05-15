using Book.Service.Entities;
using Book.Service.Dtos;

namespace Book.Service.Extenstions {
    public static class Extenstions {

        public static BooksDto AsDto(this SingleBook singleBook) {
            return new BooksDto(singleBook.Id, singleBook.BookName, singleBook.Author, singleBook.Price, singleBook.ReleaseDate);
        }
    }
}