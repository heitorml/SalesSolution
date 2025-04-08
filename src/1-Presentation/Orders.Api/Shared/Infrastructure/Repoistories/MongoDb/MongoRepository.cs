using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Orders.Api.Shared.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Orders.Api.Shared.Repoistories.MongoDb
{
    [ExcludeFromCodeCoverage]
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(IOptions<MongoDbSettings> mongoSettings)
        {
            var settings = mongoSettings.Value;
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }

        public async Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default)
            => await _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync(cancellationToken);

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
            => await _collection.Find(_ => true).ToListAsync(cancellationToken);

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
            => await _collection.InsertOneAsync(entity);

        public async Task UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
            => await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq(e => e.Id, id), entity);

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
            => await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id), cancellationToken);

        public async Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _collection.Find(predicate).ToListAsync(cancellationToken);

        public async Task<T> FirstOrDefault(
            Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            => await _collection.Find(predicate).FirstOrDefaultAsync(cancellationToken);

    }
}
