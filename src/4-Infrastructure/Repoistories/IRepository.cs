using CrossCutting;
using System.Linq.Expressions;

namespace Infrastructure.Repoistories
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateAsync(string id, T entity,CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
