using Book.Service.Entities;
using MongoDB.Driver;

namespace Book.Service.Repositories
{

    public class BookRepository {

        private const string collectionName = "books";

        private readonly IMongoCollection<singleBook>? dbCollection;

    }
}
