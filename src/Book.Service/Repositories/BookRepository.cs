using Book.Service.Entities;
using MongoDB.Driver;

namespace Book.Service.Repositories
{

    public class BookRepository {

        private const string collectionName = "books";

        private readonly IMongoCollection<SingleBook>? dbCollection;

        private readonly FilterDefinitionBuilder<SingleBook> filterBuilder = Builders<SingleBook>.Filter;

        public BookRepository() {
            MongoClient mongoClient = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase database = mongoClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<SingleBook>(collectionName);
        }

        public async Task<IReadOnlyCollection<SingleBook>> GetAllAsync() {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<SingleBook> GetAsync(int id) {
            FilterDefinition<SingleBook> filter = filterBuilder.Eq(entity => entity.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(SingleBook entity) {

            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            await dbCollection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(SingleBook entity) {

             if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            FilterDefinition<SingleBook> filter = filterBuilder.Eq(existingBook => existingBook.Id, entity.Id);
            await dbCollection.ReplaceOneAsync(filter, entity);
        }

        public async Task RemoveAsync(int id) {
            FilterDefinition<SingleBook> filter = filterBuilder.Eq(entity => entity.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
