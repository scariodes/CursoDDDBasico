using Api.Domain.Entities;

namespace Api.Domain.Interfaces
{
    public interface IRepository<T> where T:BaseEntity
    {
        public Task<T> InsertAsync(T item);
        public Task<T> UpdateAsync(T item);
        public Task<bool> DeleteAsync(Guid id);
        public Task<T> SelectAsync(Guid id);
        public Task<IEnumerable<T>> SelectAsync();
    }
}